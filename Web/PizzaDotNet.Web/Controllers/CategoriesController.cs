namespace PizzaDotNet.Web.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Categories;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new CategoriesViewModel
            {
                Categories =
                    this.categoriesService.GetAll<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        // public IActionResult Details(int id)
        // {
        //     var categoryVeiwModel = new CategoryViewModel();
        //     return this.View(null)
        // }
    }
}