using System;
using PizzaDotNet.Data.Models.Enums;

namespace PizzaDotNet.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Common;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Administration.Orders;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class OrdersController : AdministrationController
    {
        private static string CANNOT_DELETE_ORDER = "Orders can not be deleted";
        private static string CANNOT_EDIT_ORDER = "Feature not implemented yet";

        private readonly IMapper mapper;
        private readonly IOrdersService ordersService;
        private readonly IOrderStatusService orderStatusService;

        public OrdersController(
            IMapper mapper,
            IOrdersService ordersService,
            IOrderStatusService orderStatusService)
        {
            this.mapper = mapper;
            this.ordersService = ordersService;
            this.orderStatusService = orderStatusService;
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
            var order = this.ordersService.GetBaseById(orderId);
            var statuses = this.orderStatusService.GetAll<AdminOrderStatusViewModel>();
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
        public IActionResult Edit(AdminOrderInputModel inputModel)
        {
            var order = this.ordersService.GetBaseById(inputModel.Id);
            var orderStatus = this.orderStatusService.GetById(inputModel.OrderStatusId);

            order.OrderStatus = orderStatus;

            this.ordersService.UpdateAsync(order);

            return this.RedirectToAction("View", new { orderId = order.Id });
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

