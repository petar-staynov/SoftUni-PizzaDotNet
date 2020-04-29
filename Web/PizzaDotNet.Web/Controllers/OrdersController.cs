namespace PizzaDotNet.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Orders;

    [Authorize]
    public class OrdersController : BaseController
    {
        private const string ACCESS_DENY_VIEW_ORDER = "You're not allowed to view this order";
        private const string ORDER_CANCELLED = "Your has been cancelled";
        private const string ORDER_CANT_CANCEL = "This order cannot be canceled";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly IAddressesService addressesService;
        private readonly IOrderStatusService orderStatusService;
        private readonly IOrdersService ordersService;
        private readonly ISessionService sessionService;
        private readonly IProductsService productsService;
        private readonly IProductSizeService productSizeService;
        private readonly ICouponCodeService couponCodeService;

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAddressesService addressesService,
            IOrderStatusService orderStatusService,
            IOrdersService ordersService,
            ISessionService sessionService,
            IProductsService productsService,
            IProductSizeService productSizeService,
            ICouponCodeService couponCodeService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.addressesService = addressesService;
            this.orderStatusService = orderStatusService;
            this.ordersService = ordersService;
            this.sessionService = sessionService;
            this.productsService = productsService;
            this.productSizeService = productSizeService;
            this.couponCodeService = couponCodeService;
        }

        public async Task<IActionResult> PlaceOrder(CartViewModel inputModel)
        {
            /* Get User */
            var user = await this.userManager.GetUserAsync(this.User);
            var userId = user.Id;

            /* Map cart address to order address */
            var orderAddress = this.mapper.Map<OrderAddress>(inputModel.Address);

            /* Create/Update user address if box was checked */
            if (inputModel.UseAddress == true)
            {
                var userAddress = this.addressesService.GetBaseByUserId(userId);
                if (userAddress == null)
                {
                    var newUserAddress = this.mapper.Map<UserAddress>(inputModel.Address);
                    newUserAddress.User = user;
                    userAddress = await this.addressesService.CreateAsync(newUserAddress);
                }
                else
                {
                    /* Map from view model to model without losing data*/
                    var newUserAddress = this.mapper.Map(inputModel.Address, userAddress);
                    await this.addressesService.UpdateAsync(newUserAddress);
                }
            }

            /* Get order status of "Processing" */
            var orderStatus = this.orderStatusService.GetByName(OrderStatusEnum.Processing.ToString());

            /* Map order products */
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            var orderProducts = new List<OrderProduct>();
            foreach (SessionCartProductDto productDto in cart.Products)
            {
                /* Create OrderProduct */
                var product = this.productsService.GetBaseById(productDto.Id);
                var orderProduct = this.mapper.Map<OrderProduct>(product);

                orderProduct.Product = product;

                /* Get product size */
                var productSize =
                    this.productSizeService.GetProductSizeBase(productDto.Id, productDto.SizeName);
                orderProduct.Size = productSize.Name;

                /* Map price*/
                orderProduct.Price = productSize.Price;

                /* Map Quantity, Size from Product DTO */
                orderProduct = this.mapper.Map(productDto, orderProduct);

                orderProducts.Add(orderProduct);
            }

            decimal orderTotalPrice = orderProducts.Select(p => p.Price).Sum();


            /* Apply discount code */
            CouponCode couponCode = null;
            decimal? orderTotalDiscountPrice = orderTotalPrice;
            var sessionCouponCode =
                this.sessionService.Get<SessionCouponCodeDto>(this.HttpContext.Session, "CouponCode");
            if (sessionCouponCode != null)
            {
                couponCode = this.couponCodeService.GetBaseByCode(sessionCouponCode.Code);

                orderTotalDiscountPrice =
                    orderTotalPrice * (decimal?)(1F - (couponCode.DiscountPercent / 100F));
            }

            var order = new Order()
            {
                User = user,
                OrderAddress = orderAddress,
                OrderStatus = orderStatus,
                OrderProducts = orderProducts,
                TotalPrice = orderTotalPrice,
                CouponCode = couponCode,
                TotalPriceDiscounted = orderTotalDiscountPrice,
                OrderNotes = inputModel.AdditionalNotes,
            };
            var orderEntity = await this.ordersService.CreateAsync(order);

            /* Disable coupon code */
            if (couponCode != null)
            {
                this.couponCodeService.UseCodeByCode(couponCode.Code);
            }

            return this.RedirectToAction("View", new{ orderId = orderEntity.Id });
        }

        public IActionResult View(int orderId)
        {
            var userIsAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);
            var userId = this.userManager.GetUserId(this.User);
            var orderViewModel = this.ordersService.GetById<OrderViewModel>(orderId);

            /* Prevent people (except admin) from viewing others orders */
            if (!userIsAdmin && userId != orderViewModel.UserId)
            {
                this.TempData["Message"] = ACCESS_DENY_VIEW_ORDER;
                this.TempData["MessageType"] = AlertMessageTypes.Error;
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(orderViewModel);
        }

        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var userIsAdmin = this.User.IsInRole(GlobalConstants.AdministratorRoleName);
            var userId = this.userManager.GetUserId(this.User);
            var order = this.ordersService.GetById<OrderDto>(orderId);

            /* Prevent people from cancelling others orders */
            if (!userIsAdmin && userId != order.UserId)
            {
                this.TempData["Message"] = ACCESS_DENY_VIEW_ORDER;
                this.TempData["MessageType"] = AlertMessageTypes.Error;
                return this.RedirectToAction("Index", "Home");
            }

            var orderStatus = order.OrderStatus;
            if (orderStatus.Status != OrderStatusEnum.Processing.ToString())
            {
                this.TempData["Message"] = ORDER_CANT_CANCEL;
                this.TempData["MessageType"] = AlertMessageTypes.Error;
                return this.RedirectToAction("View", new { orderId = orderId });
            }

            await this.ordersService.ChangeStatus(orderId, OrderStatusEnum.Cancelled);


            this.TempData["Message"] = ORDER_CANCELLED;
            this.TempData["MessageType"] = AlertMessageTypes.Error;
            return this.RedirectToAction("View", new { orderId = orderId });
        }
    }
}
