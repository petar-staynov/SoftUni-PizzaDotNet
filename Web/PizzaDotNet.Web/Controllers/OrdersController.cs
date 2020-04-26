using PizzaDotNet.Services;
using PizzaDotNet.Web.ViewModels.DTO;
using PizzaDotNet.Web.ViewModels.Order;

namespace PizzaDotNet.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Cart;

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

        public OrdersController(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IAddressesService addressesService,
            IOrderStatusService orderStatusService,
            IOrdersService ordersService,
            ISessionService sessionService,
            IProductsService productsService,
            IProductSizeService productSizeService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.addressesService = addressesService;
            this.orderStatusService = orderStatusService;
            this.ordersService = ordersService;
            this.sessionService = sessionService;
            this.productsService = productsService;
            this.productSizeService = productSizeService;
        }

        public async Task<IActionResult> PlaceOrder(CartViewModel inputModel)
        {
            /*
             * public string UserId { get; set; }
             * public ApplicationUser User { get; set; }
             * public virtual OrderAddress Address { get; set; }
             * public int OrderStatusId { get; set; }
             * public virtual OrderStatus OrderStatus { get; set; }
             * public ICollection<OrderProduct> OrderProducts { get; set; }
             * public decimal TotalPrice { get; set; }
             */

            var userId = this.userManager.GetUserId(this.User);

            /* Create user address if there is none */
            var userAddress = this.addressesService.GetBaseByUserId(userId);
            if (userAddress == null)
            {
                var newUserAddress = this.mapper.Map<UserAddress>(inputModel.Address);
                userAddress = await this.addressesService.CreateAddressAsync(newUserAddress);
            }

            /* Map cart address to order address. Assign AddressId for use in composite key */
            var orderAddress = this.mapper.Map<OrderAddress>(inputModel.Address);
            orderAddress.UserAddressId = userAddress.Id;

            /* Get order status of "Processing" */
            var orderStatus = this.orderStatusService.GetByName(OrderStatusEnum.Processing.ToString());

            /* Map order products */
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, "Cart");

            var orderProducts = new List<OrderProduct>();
            foreach (SessionCartProductDto productDto in cart.Products)
            {
                /* Create view model */
                var orderProduct = this.productsService.GetById<OrderProduct>(productDto.Id);

                /* Get product size */
                var productSize =
                    this.productSizeService.GetProductSizeBase(productDto.Id, productDto.SizeString);
                orderProduct.Size = productSize.Size;

                /* Map data (Quantity, Size) from Product DTO */
                orderProduct = this.mapper.Map(productDto, orderProduct);

                orderProducts.Add(orderProduct);
            }


            var order = new Order()
            {
                UserId = userId,
                Address = orderAddress,
                OrderStatus = orderStatus,
                OrderProducts = orderProducts,
                TotalPrice = inputModel.TotalPrice,
                TotalPriceDiscounted = inputModel.DiscountPrice,
                OrderNotes = inputModel.AdditionalNotes,
            };

            await this.ordersService.CreateAsync(order);

            return this.RedirectToAction("ViewOrder", order);
        }

        public IActionResult ViewOrder(Order order)
        {
            var oderViewModel = this.mapper.Map<OrderViewModel>(order);
            return this.View();
        }
    }
}
