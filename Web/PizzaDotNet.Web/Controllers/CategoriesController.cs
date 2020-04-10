namespace PizzaDotNet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Categories;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IProductsService productsService)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
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

        [Route("[controller]/{name}")]
        public IActionResult Details(string name)
        {
            var categoryViewModel = this.categoriesService.GetByName<CategoryViewModel>(name);
            if (categoryViewModel == null)
            {
                return this.NotFound();
            }

            int categoryId = categoryViewModel.Id;
            categoryViewModel.Products =
                this.productsService.GetByCategoryId<CategoryProductViewModel>(categoryId);

            return this.View(categoryViewModel);
        }
    }
}
