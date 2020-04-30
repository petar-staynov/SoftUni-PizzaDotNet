namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Administration.Categories;
    using PizzaDotNet.Web.ViewModels.Administration.Products;
    using PizzaDotNet.Web.ViewModels.Administration.Shared;

    public class CategoriesController : AdministrationController
    {
        private const string CATEGORY_CREATE_SUCCESS = "Category created succesfully";
        private const string CATEGORY_EDIT_SUCCESS = "Category edited succesfully";
        private const string CATEGORY_DELETE_SUCCESS = "Category deleted succesfully";

        private readonly IMapper mapper;
        private readonly IGoogleCloudStorage googleCloudStorage;
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public CategoriesController(
            IMapper mapper,
            IGoogleCloudStorage googleCloudStorage,
            ICategoriesService categoriesService,
            IProductsService productsService)
        {
            this.mapper = mapper;
            this.googleCloudStorage = googleCloudStorage;
            this.categoriesService = categoriesService;
            this.productsService = productsService;
        }

        private async Task UploadProductImage(ImageUploadInputModel imageModel)
        {
            string fileNameForStorage = imageModel.ImageFile.FileName;
            imageModel.ImageUrl = await this.googleCloudStorage.UploadFileAsync(imageModel.ImageFile, fileNameForStorage);
            imageModel.ImageStorageName = fileNameForStorage;
        }

        public async Task<IActionResult> Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All(string sortingCriteria = null)
        {
            var categories =
                await this.categoriesService.GetAll<AdminCategoriesCategoryViewModel>(sortingCriteria);

            var viewModel = new AdminCategoriesViewModel()
            {
                Categories = categories,
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
        public async Task<IActionResult> View(int categoryId, string sortingCriteria = null)
        {
            var products =
                await this.productsService.GetByCategoryId<AdminCategoryProductViewModel>(categoryId, sortingCriteria);

            var categoryViewModel =
                await this.categoriesService.GetById<AdminCategoryViewModel>(categoryId);

            categoryViewModel.Products = products;


            return this.View(categoryViewModel);
        }

        [HttpPost]
        public IActionResult ViewPost(AdminCategoryViewModel inputModel)
        {
            var sortingCriteria = inputModel.SortingCriteria;
            return this.RedirectToAction("View", new { categoryId = inputModel.Id, sortingCriteria = sortingCriteria });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new AdminCategoryCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCategoryCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
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

            var category = new Category
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                ImageUrl = imageUploadModel.ImageUrl,
                ImageStorageName = imageUploadModel.ImageStorageName,
            };

            await this.categoriesService.CreateAsync(category);

            this.TempData["Message"] = CATEGORY_CREATE_SUCCESS;
            this.TempData["MessageType"] = AlertMessageTypes.Success;

            return this.RedirectToAction("View", new { categoryId = category.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int categoryId)
        {
            var categoryInputModel =
                await this.categoriesService.GetById<AdminCategoryEditInputModel>(categoryId);
            var imageModel =
                await this.categoriesService.GetById<ImageUploadInputModel>(categoryId);
            categoryInputModel.ImageModel = imageModel;

            return this.View(categoryInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminCategoryEditInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var category = await this.categoriesService.GetBaseById(inputModel.Id);

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
                    category.ImageUrl = imageUploadModel.ImageUrl;
                    category.ImageStorageName = imageUploadModel.ImageStorageName;
                }
            }

            await this.categoriesService.UpdateAsync(category);

            this.TempData["Message"] = CATEGORY_EDIT_SUCCESS;
            this.TempData["MessageType"] = AlertMessageTypes.Success;

            // Custom route redirect because it matches the normal ProductsController action
            return this.RedirectToRoute(new
            {
                area = "Administration",
                controller = "Categories",
                action = "View",
                categoryId = category.Id,
            });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var categoryInputModel =
                await this.categoriesService.GetById<AdminCategoryEditDeleteViewModel>(categoryId);

            var imageModel = await this.categoriesService.GetById<ImageUploadInputModel>(categoryId);
            categoryInputModel.ImageModel = imageModel;

            return this.View(categoryInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AdminCategoryEditDeleteViewModel inputModel)
        {
            var categoryId = inputModel.Id;
            var category =
                await this.categoriesService.GetBaseById(categoryId);

            /* Delete products */
            await this.productsService.DeleteByCategoryIdAsync(categoryId);

            /* Delete category */
            await this.categoriesService.DeleteAsync(category);

            return this.RedirectToAction("All");
        }
    }
}
