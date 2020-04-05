namespace PizzaDotNet.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services.Data;

    [ApiController]
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            return null;
            // var viewModel = new CategoriesViewModel
            // {
            //     Categories =
            //         this.categoriesService.GetAll<CategoriesCategoryViewModel>(),
            // };
            //
            // return this.View(viewModel);
        }


        public IActionResult Details(string name)
        {
            return null;
            // Get category from service by name
            // Get products in category
            // Add them to CategoryProductsViewModel
            // Create ProductViewModel
            
            // var categoryViewModel = this.categoriesService.GetByName<CategoryViewModel>(name);
            //
            // return this.View(categoryViewModel);
        }
    }
}
