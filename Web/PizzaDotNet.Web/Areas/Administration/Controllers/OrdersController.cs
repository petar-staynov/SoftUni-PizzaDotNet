using System.Threading.Tasks;
using PizzaDotNet.Common;

namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class OrdersController : AdministrationController
    {
        private static string CANNOT_DELETE_ORDER = "Orders can not be deleted";
        private static string CANNOT_EDIT_ORDER = "Feature not implemented yet";

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
            var orderViewModel = this.ordersService.GetById<AdminOrderViewModel>(orderId);

            this.TempData["Message"] = CANNOT_EDIT_ORDER;
            this.TempData["MessageType"] = AlertMessageTypes.Error;
            return this.RedirectToAction("View", new { orderId = orderId });
        }

        public IActionResult Delete(int orderId)
        {
            var orderViewModel = this.ordersService.GetById<AdminOrderViewModel>(orderId);

            this.TempData["Message"] = CANNOT_DELETE_ORDER;
            this.TempData["MessageType"] = AlertMessageTypes.Error;
            return this.RedirectToAction("View", new { orderId = orderId });
        }

        // [HttpPost]
        // public async Task<ActionResult> Delete(AdminOrderViewModel inputModel)
        // {
        //     var orderId = inputModel.Id;
        //
        //     var orderViewModel = await this.ordersService.DeleteAsync(orderId);
        //
        //     this.TempData["Message"] = ORDER_DELETED;
        //     this.TempData["MessageType"] = AlertMessageTypes.Success;
        //     return this.RedirectToAction("All");
        // }
    }
}

