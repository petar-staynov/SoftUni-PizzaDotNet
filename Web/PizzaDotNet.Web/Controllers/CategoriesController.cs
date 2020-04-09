namespace PizzaDotNet.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services.Data;
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
                    this.categoriesService.GetAll<CategoriesCategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Details(string name)
        {
            // Get category from service by name
            // Get products in category
            // Add them to CategoryProductsViewModel
            // Create ProductViewModel
            var categoryViewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            return this.View(categoryViewModel);
        }
    }
}
