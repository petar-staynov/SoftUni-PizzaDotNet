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
            /*
             * string UserId { get; set; }
             * virtual ApplicationUser User { get; set; }
             * OrderAddress Address { get; set; }
             * int OrderStatusId { get; set; }
             * virtual OrderStatus OrderStatus { get; set; }
             * ICollection<OrderProduct> OrderProducts { get; set; }
             * decimal? TotalPrice => this.OrderProducts.Select(p => p.Price * p.Quantity).Sum();
             * int CouponCodeId { get; set; }
             * virtual CouponCode CouponCode { get; set; }
             * float DiscountPercent { get; set; }
             * decimal? TotalPriceDiscounted 
             * public string OrderNotes { get; set; }
             */
            var order = new Order();

            var userId = this.userManager.GetUserId(this.User);

            order.UserId = userId;

            /* Map cart address to order address. Assign AddressId for use in composite key */
            var orderAddress = this.mapper.Map<OrderAddress>(inputModel.Address);
            order.Address = orderAddress;

            /* Create/Update user address if box was checked */
            // TODO Only if box is checked
            if (inputModel.UseAddress == true)
            {
                var userAddress = this.addressesService.GetBaseByUserId(userId);
                if (userAddress == null)
                {
                    var newUserAddress = this.mapper.Map<UserAddress>(inputModel.Address);
                    userAddress = await this.addressesService.CreateAddressAsync(newUserAddress);
                }
                else
                {
                    /* Map from view model to model without losing data*/
                    var newUserAddress = this.mapper.Map(inputModel.Address, userAddress);
                    await this.addressesService.UpdateAddressAsync(newUserAddress);
                }

                orderAddress.UserAddressId = userAddress.Id;
            }

            /* Get order status of "Processing" */
            var orderStatus = this.orderStatusService.GetByName(OrderStatusEnum.Processing.ToString());
            order.OrderStatus = orderStatus;

            /* Map order products */
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, "Cart");
            var orderProducts = new List<OrderProduct>();
            foreach (SessionCartProductDto productDto in cart.Products)
            {
                /* Create OrderProduct */
                var product = this.productsService.GetBaseById(productDto.Id);
                var orderProduct = this.mapper.Map<OrderProduct>(product);

                orderProduct.Product = product;
                
                /* Get product size */
                var productSize =
                    this.productSizeService.GetProductSizeBase(productDto.Id, productDto.SizeString);
                orderProduct.Size = productSize.Size;

                /* Map price*/
                orderProduct.Price = productSize.Price;
                
                /* Map Quantity, Size from Product DTO */
                orderProduct = this.mapper.Map(productDto, orderProduct);

                orderProducts.Add(orderProduct);
            }

            order.OrderProducts = orderProducts;

            /* Apply discount code */
            var sessionCouponCode =
                this.sessionService.Get<SessionCouponCodeDto>(this.HttpContext.Session, "CouponCode");
            if (sessionCouponCode != null)
            {
                var couponCode = this.couponCodeService.GetBaseByCode(sessionCouponCode.Code);
                if (couponCode != null)
                {
                    order.CouponCode = couponCode;
                    order.DiscountPercent = couponCode.DiscountPercent;
                    order.CouponCodeString = couponCode.Code;
                    order.DiscountPercent = couponCode.DiscountPercent;
                }
            }

            order.OrderNotes = inputModel.AdditionalNotes;

            var orderEntity = await this.ordersService.CreateAsync(order);

            /* Disable coupon code */
            if (orderEntity.Id != null)
            {
                this.couponCodeService.UseCodeByCode(orderEntity.CouponCodeString);
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
