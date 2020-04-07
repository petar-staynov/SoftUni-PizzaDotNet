﻿namespace PizzaDotNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Categories;
    using PizzaDotNet.Web.ViewModels.Products;

    public class ProductsController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            throw new NotImplementedException();
        }

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
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }


            var productId = await this.productsService.CreateAsync(
                inputModel.Name,
                inputModel.Description,
                inputModel.Price,
                inputModel.ImageUrl,
                inputModel.CategoryId);

            return this.RedirectToAction("ViewById", new { id = productId });
        }
    }
}
