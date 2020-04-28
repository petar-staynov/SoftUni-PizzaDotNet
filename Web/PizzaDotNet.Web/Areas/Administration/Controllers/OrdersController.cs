namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class OrdersController : AdministrationController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }


        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult All(string sortingCriteria = null)
        {
            var orders =
                this.ordersService.GetAll<AdminOrdersOrderListItemViewModel>(sortingCriteria);

            var viewModel = new AdminOrdersViewModel()
            {
                Orders = orders,
                OrdersCount = orders.Count(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult All(AdminOrdersViewModel inputModel)
        {
            var sortingCriteria = inputModel.SortingCriteria;
            return this.RedirectToAction("All", new { sortingCriteria = sortingCriteria });
        }


        public IActionResult View(int orderId)
        {
            var orderViewModel = this.ordersService.GetById<AdminOrderViewModel>(orderId);

            return this.View(orderViewModel);
        }

        public IActionResult Edit(int orderId)
        {
            return null;
        }

        public IActionResult Delete(int orderId)
        {
            return null;
        }
    }
}

