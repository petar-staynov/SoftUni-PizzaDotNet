namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly IOrdersService ordersService;

        public DashboardController(
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService,
            IProductsService productsService,
            IOrdersService ordersService)
        {
            this.userManager = userManager;
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.ordersService = ordersService;
        }

        public IActionResult Index()
        {
            var usersCount = this.userManager.Users.Count();
            var categoriesCount = this.categoriesService.GetCount();
            var productsCounts = this.productsService.GetCount();
            var ordersCounts = this.ordersService.GetCount();

            decimal totalProfits = this.ordersService.GetTotalProfit() ?? 0M;

            var viewModel = new IndexViewModel
            {
                UsersCount = usersCount,
                CategoriesCount = categoriesCount,
                ProductsCount = productsCounts,
                OrdersCount = ordersCounts,
                OrdersTotalProfits = totalProfits,
            };

            return this.View(viewModel);
        }
    }
}
