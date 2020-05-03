using PizzaDotNet.Data.Models.Enums;

namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Common;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Administration.Orders;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class OrdersController : AdministrationController
    {
        private const string CANNOT_DELETE_ORDER = "Orders can not be deleted";
        private const string CANCEL_EDIT = "Editing canceled";

        private readonly IMapper mapper;
        private readonly IOrdersService ordersService;
        private readonly IOrderStatusService orderStatusService;
        private readonly ICouponCodeService couponCodeService;

        public OrdersController(
            IMapper mapper,
            IOrdersService ordersService,
            IOrderStatusService orderStatusService,
            ICouponCodeService couponCodeService)
        {
            this.mapper = mapper;
            this.ordersService = ordersService;
            this.orderStatusService = orderStatusService;
            this.couponCodeService = couponCodeService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All(string sortingCriteria = null)
        {
            var orders =
                await this.ordersService.GetAll<AdminOrdersOrderListItemViewModel>(sortingCriteria);

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

        public async Task<IActionResult> View(int orderId)
        {
            var orderViewModel = await this.ordersService.GetById<AdminOrderViewModel>(orderId);

            return this.View(orderViewModel);
        }

        public async Task<IActionResult> Edit(int orderId)
        {
            var order = await this.ordersService.GetBaseById(orderId);
            var statuses = await this.orderStatusService.GetAll<AdminOrderStatusViewModel>();
            var orderProducts = this.mapper.Map<List<AdminOrderProductViewModel>>(order.OrderProducts);

            var orderViewModel = new AdminOrderInputModel
            {
                Id = order.Id,
                UserId = order.User.Id,
                UserName = order.User.UserName,
                OrderAddress = order.OrderAddress,
                OrderStatusId = order.OrderStatusId,
                OrderStatuses = statuses,
                OrderProducts = orderProducts,
                CouponCodeId = order.CouponCodeId,
                CouponCode = order.CouponCode,
                TotalPrice = order.TotalPrice,
                TotalPriceDiscounted = order.TotalPriceDiscounted,
                OrderNotes = order.OrderNotes,
            };

            return this.View(orderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminOrderInputModel inputModel)
        {
            var order = await this.ordersService.GetBaseById(inputModel.Id);
            var orderStatus = await this.orderStatusService.GetById(inputModel.OrderStatusId);

            order.OrderStatus = orderStatus;

            await this.ordersService.UpdateAsync(order);

            return this.RedirectToAction("View", new { orderId = order.Id });
        }

        public IActionResult EditCancel(int orderId)
        {
            this.TempData["Message"] = CANCEL_EDIT;
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

        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            /* Update order status */
            var order = await this.ordersService.GetBaseById(orderId);
            order.OrderStatus = await this.orderStatusService.GetByName(OrderStatusEnum.Delivered.ToString());

            /* Give CouponCode ot user */
            var userId = order.UserId;
            var couponCode =
                this.couponCodeService.GenerateCouponCodeForUser(GlobalConstants.StandardDiscountPercent, userId);

            /* Redirect to last action */
            return this.Redirect(this.Request.Headers["Referer"].ToString());
        }
    }
}
