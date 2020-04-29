namespace PizzaDotNet.Web.Controllers
{
    using System.Threading.Tasks;

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

        [Route("[controller]")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new CategoriesViewModel
            {
                Categories =
                   await this.categoriesService.GetAll<CategoriesCategoryViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Route("[controller]/{name}")]
        public async Task<IActionResult> Details(string name, string sortingCriteria = null)
        {
            var categoryViewModel = await this.categoriesService.GetByName<CategoryViewModel>(name);
            if (categoryViewModel == null)
            {
                return this.NotFound();
            }

            int categoryId = categoryViewModel.Id;
            categoryViewModel.Products =
              await this.productsService.GetByCategoryId<CategoryProductViewModel>(categoryId, sortingCriteria);

            return this.View(categoryViewModel);
        }

        [HttpPost]
        [Route("[controller]/{name}")]
        public IActionResult Details(CategoryViewModel viewModel)
        {
            var sortingCriteria = viewModel.SortingCriteria;
            return this.RedirectToAction("Details", new { sortingCriteria = sortingCriteria });
        }
    }
}
