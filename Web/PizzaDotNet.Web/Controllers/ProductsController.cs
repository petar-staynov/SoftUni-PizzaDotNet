using PizzaDotNet.Web.ViewModels.ProductSize;

namespace PizzaDotNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Categories;
    using PizzaDotNet.Web.ViewModels.Products;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IRatingsService ratingsService;
        private readonly IProductSizeService productSizeService;

        private readonly IGoogleCloudStorage googleCloudStorage;
        private readonly IMapper mapper;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            IRatingsService ratingsService,
            IProductSizeService productSizeService,
            IGoogleCloudStorage googleCloudStorage,
            IMapper mapper)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.ratingsService = ratingsService;
            this.productSizeService = productSizeService;
            this.googleCloudStorage = googleCloudStorage;
            this.mapper = mapper;
        }

        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }

        public IActionResult Index()
        {
            throw new NotImplementedException();
        }

        private async Task UploadProductImage(ProductCreateInputModel product)
        {
            string fileNameForStorage = FormFileName(product.Name, product.ImageFile.FileName);
            product.ImageUrl = await this.googleCloudStorage.UploadFileAsync(product.ImageFile, fileNameForStorage);
            product.ImageStorageName = fileNameForStorage;
        }

        [Route("[controller]/{id}")]
        public IActionResult ViewById(int id)
        {
            var productViewModel = this.productsService.GetById<ProductViewInputModel>(id);
            if (productViewModel == null)
            {
                return this.NotFound();
            }

            productViewModel.Rating = this.ratingsService.GetProductRating(id);
            productViewModel.Sizes = this.productSizeService.GetAllProductSizes<ProductSizeViewModel>(id);

            return this.View(productViewModel);
        }

        // [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var categories =
                this.categoriesService.GetAll<CategoryDropdownViewModel>();

            var viewModel = new ProductCreateInputModel();
            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // Pass back the categories list because POST Requests lose Collections
                inputModel.Categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
                return this.View(inputModel);
            }

            if (inputModel.ImageFile != null)
            {
                await this.UploadProductImage(inputModel);
            }

            var filteredSizesInputs = inputModel.Sizes
                .Where(s => !String.IsNullOrEmpty(s.Size) && s.Price >= 0M).ToList();

            List<ProductSize> sizes = this.mapper.Map<List<ProductSize>>(filteredSizesInputs);

            var product = await this.productsService.CreateAsync(
                    inputModel.Name,
                    inputModel.Description,
                    inputModel.CategoryId,
                    sizes,
                    inputModel.ImageUrl,
                    inputModel.ImageStorageName);

            return this.RedirectToAction("ViewById", new { id = product.Id });
        }
    }
}
