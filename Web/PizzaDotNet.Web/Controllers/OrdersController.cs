using PizzaDotNet.Common;

namespace PizzaDotNet.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Order;

    [Authorize]
    public class OrdersController : BaseController
    {
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
                    userAddress = await this.addressesService.CreateAddressAsync(newUserAddress);
                }
                else
                {
                    /* Map from view model to model without losing data*/
                    var newUserAddress = this.mapper.Map(inputModel.Address, userAddress);
                    await this.addressesService.UpdateAddressAsync(newUserAddress);
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


            /* Apply discount code */
            CouponCode couponCode = null;
            var sessionCouponCode =
                this.sessionService.Get<SessionCouponCodeDto>(this.HttpContext.Session, "CouponCode");
            if (sessionCouponCode != null)
            {
                couponCode = this.couponCodeService.GetBaseByCode(sessionCouponCode.Code);
            }

            var order = new Order()
            {
                User = user,
                OrderAddress = orderAddress,
                OrderStatus = orderStatus,
                OrderProducts = orderProducts,
                CouponCode = couponCode,
                OrderNotes = inputModel.AdditionalNotes,
            };
            var orderEntity = await this.ordersService.CreateAsync(order);

            /* Disable coupon code */
            if (couponCode != null)
            {
                this.couponCodeService.UseCodeByCode(inputModel.CouponCode);
            }

            var orderViewModel = this.mapper.Map<OrderViewModel>(orderEntity);
            return this.RedirectToAction("ViewOrder", orderViewModel);
        }

        public IActionResult ViewOrder(OrderViewModel orderViewModel)
        {
            return this.View(orderViewModel);
        }
    }
}
