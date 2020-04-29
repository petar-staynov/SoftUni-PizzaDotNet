namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Administration.Products;
    using PizzaDotNet.Web.ViewModels.Administration.Shared;
    using PizzaDotNet.Web.ViewModels.Categories;

    public class ProductsController : AdministrationController
    {
        private const string CANCEL_EDIT = "Editing canceled";
        private const string CANCEL_DELETE = "Deleting canceled";
        private const string PRODUCT_DELETED = "Product deleted successfully";

        private readonly IMapper mapper;
        private readonly IGoogleCloudStorage googleCloudStorage;
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly IRatingsService ratingsService;
        private readonly IProductSizeService productSizeService;

        public ProductsController(
            IMapper mapper,
            IGoogleCloudStorage googleCloudStorage,
            ICategoriesService categoriesService,
            IProductsService productsService,
            IRatingsService ratingsService,
            IProductSizeService productSizeService)
        {
            this.mapper = mapper;
            this.googleCloudStorage = googleCloudStorage;
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.ratingsService = ratingsService;
            this.productSizeService = productSizeService;
        }

        private static string FormFileName(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
            return fileNameForStorage;
        }

        private async Task UploadProductImage(ImageUploadInputModel imageModel)
        {
            string fileNameForStorage = imageModel.ImageFile.FileName;
            imageModel.ImageUrl = await this.googleCloudStorage.UploadFileAsync(imageModel.ImageFile, fileNameForStorage);
            imageModel.ImageStorageName = fileNameForStorage;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All(string sortingCriteria = null)
        {
            var products =
                await this.productsService.GetAll<AdminProductsProductViewModel>(sortingCriteria);

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
        public async Task<IActionResult> View(int productId)
        {
            var productViewModel = await this.productsService.GetById<AdminProductViewModel>(productId);
            if (productViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(productViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories =
                await this.categoriesService.GetAll<CategoryDropdownViewModel>();

            var viewModel = new AdminProductCreateInputModel();
            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminProductCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // Pass back the categories list because POST Requests lose Collections
                var categories =
                    await this.categoriesService.GetAll<CategoryDropdownViewModel>();
                inputModel.Categories = categories;
                return this.View(inputModel);
            }

            var imageUploadModel = new ImageUploadInputModel();
            if (inputModel.ImageModel.ImageFile != null)
            {
                imageUploadModel = new ImageUploadInputModel
                {
                    ImageFile = inputModel.ImageModel.ImageFile,
                };
                await this.UploadProductImage(imageUploadModel);
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
                ImageUrl = imageUploadModel.ImageUrl,
                ImageStorageName = imageUploadModel.ImageStorageName,
            };

            await this.productsService.CreateAsync(product);

            return this.RedirectToAction("View", new { id = product.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            var productInputModel = await this.productsService.GetById<AdminProductEditInputModel>(productId);

            var imageModel = await this.productsService.GetById<ImageUploadInputModel>(productId);
            productInputModel.ImageModel = imageModel;

            var categories =
                await this.categoriesService.GetAll<CategoryDropdownViewModel>();
            productInputModel.Categories = categories;

            return this.View(productInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminProductEditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // Pass back the categories and Sizes list because POST Requests lose Collections
                var categories =
                   await this.categoriesService.GetAll<CategoryDropdownViewModel>();

                inputModel.Categories = categories;
                return this.View(inputModel);
            }

            var product = await this.productsService.GetBaseById(inputModel.Id);
            product.Name = inputModel.Name;
            product.Description = inputModel.Description;
            product.CategoryId = inputModel.CategoryId;

            if (inputModel.IsNewImage)
            {
                var imageUploadModel = new ImageUploadInputModel();
                if (inputModel.ImageModel.ImageFile != null)
                {
                    imageUploadModel = new ImageUploadInputModel
                    {
                        ImageFile = inputModel.ImageModel.ImageFile,
                    };
                    await this.UploadProductImage(imageUploadModel);
                    product.ImageUrl = imageUploadModel.ImageUrl;
                    product.ImageStorageName = imageUploadModel.ImageStorageName;
                }
            }

            var filteredSizesInputs = inputModel.Sizes
                .Where(s => !string.IsNullOrEmpty(s.Name) && s.Price >= 0M).ToList();

            List<ProductSize> sizes = this.mapper.Map<List<ProductSize>>(filteredSizesInputs);
            product.Sizes = sizes;

            // Delete old sizes
            await this.productSizeService.DeleteProductSizes(product.Id);
            await this.productsService.UpdateAsync(product);

            this.TempData["Message"] = PRODUCT_DELETED;
            this.TempData["MessageType"] = AlertMessageTypes.Success;

            // Custom route redirect because it matches the normal ProductsController action
            return this.RedirectToRoute(new
            {
                area = "Administration",
                controller = "Products",
                action = "View",
                productId = product.Id,
            });
        }

        public IActionResult EditCancel(int productId)
        {
            this.TempData["Message"] = CANCEL_EDIT;
            this.TempData["MessageType"] = AlertMessageTypes.Error;
            return this.RedirectToAction("View", new { productId = productId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            var productInputModel = await this.productsService.GetById<AdminProductDeleteInputModel>(productId);

            var imageModel = await this.productsService.GetById<ImageUploadInputModel>(productId);
            productInputModel.ImageModel = imageModel;

            var categories =
              await this.categoriesService.GetAll<CategoryDropdownViewModel>();
            productInputModel.Categories = categories;

            return this.View(productInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AdminProductDeleteInputModel inputModel)
        {
            var productId = inputModel.Id;

            /* Delete product sizes */
            await this.productSizeService.DeleteProductSizes(productId);

            /* Delete product */
            await this.productsService.DeleteAsync(productId);

            return this.RedirectToAction("All");
        }

        public IActionResult DeleteCancel(int productId)
        {
            this.TempData["Message"] = CANCEL_DELETE;
            this.TempData["MessageType"] = AlertMessageTypes.Error;
            return this.RedirectToAction("View", new { productId = productId });
        }
    }
}
