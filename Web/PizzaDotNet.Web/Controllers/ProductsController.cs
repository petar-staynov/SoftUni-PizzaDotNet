﻿namespace PizzaDotNet.Web.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Categories;
    using PizzaDotNet.Web.ViewModels.Products;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly IGoogleCloudStorage googleCloudStorage;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            IGoogleCloudStorage googleCloudStorage)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.googleCloudStorage = googleCloudStorage;
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
            var productViewModel = this.productsService.GetById<ProductViewModel>(id);
            if (productViewModel == null)
            {
                return this.NotFound();
            }

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
            var img = inputModel.ImageFile;
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            if (inputModel.ImageFile != null)
            {
                await this.UploadProductImage(inputModel);
            }

            var product = await this.productsService.CreateAsync(
                inputModel.Name,
                inputModel.Description,
                inputModel.Price,
                inputModel.CategoryId,
                inputModel.ImageUrl,
                inputModel.ImageFile,
                inputModel.ImageStorageName);

            return this.RedirectToAction("ViewById", new { id = product.Id });
        }
    }
}
