using PizzaDotNet.Web.ViewModels.Administration.Products;

namespace PizzaDotNet.Web.Areas.Administration.Controllers
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

    public class ProductsController : AdministrationController
    {
        private readonly IMapper mapper;
        private readonly IGoogleCloudStorage googleCloudStorage;
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public ProductsController(
            IMapper mapper,
            IGoogleCloudStorage googleCloudStorage,
            ICategoriesService categoriesService,
            IProductsService productsService)
        {
            this.mapper = mapper;
            this.googleCloudStorage = googleCloudStorage;
            this.categoriesService = categoriesService;;
            this.productsService = productsService;
        }

        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }

        private async Task UploadProductImage(ProductCreateInputModel product)
        {
            string fileNameForStorage = FormFileName(product.Name, product.ImageFile.FileName);
            product.ImageUrl = await this.googleCloudStorage.UploadFileAsync(product.ImageFile, fileNameForStorage);
            product.ImageStorageName = fileNameForStorage;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All(string sortingCriteria = null)
        {
            var products =
                this.productsService.GetAll<AdminProductsProductViewModel>(sortingCriteria);

            var viewModel = new AdminProductsViewModel()
            {
                Products = products,
                ProductsCount = products.Count(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult All(AdminProductsViewModel inputModel)
        {
            var sortingCriteria = inputModel.SortingCriteria;
            return this.RedirectToAction("All", new { sortingCriteria = sortingCriteria });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories =
                this.categoriesService.GetAll<CategoryDropdownViewModel>();

            var viewModel = new ProductCreateInputModel();
            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // Pass back the categories list because POST Requests lose Collections
                var categories =
                    this.categoriesService.GetAll<CategoryDropdownViewModel>();
                inputModel.Categories = categories;
                return this.View(inputModel);
            }

            if (inputModel.ImageFile != null)
            {
                await this.UploadProductImage(inputModel);
            }

            var filteredSizesInputs = inputModel.Sizes
                .Where(s => !string.IsNullOrEmpty(s.Name) && s.Price >= 0M).ToList();

            List<ProductSize> sizes = this.mapper.Map<List<ProductSize>>(filteredSizesInputs);

            var product = new Product
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                CategoryId = inputModel.CategoryId,
                Sizes = sizes,
                ImageUrl = inputModel.ImageUrl,
                ImageStorageName = inputModel.ImageStorageName,
            };

            await this.productsService.CreateAsync(product);

            return this.RedirectToAction("View", new { id = product.Id });
        }

        [HttpGet]
        public IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCreateInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Delete()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProductCreateInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
