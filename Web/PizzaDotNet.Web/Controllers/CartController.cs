using PizzaDotNet.Common;

namespace PizzaDotNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Addresses;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Products;
    using PizzaDotNet.Web.ViewModels.ProductSize;

    [Authorize]
    public class CartController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IAddressesService addressesService;
        private readonly IProductSizeService productSizeService;
        private readonly ICouponCodeService couponCodeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISessionService sessionService;
        private readonly IMapper mapper;

        public CartController(
            IProductsService productsService,
            IAddressesService addressesService,
            IProductSizeService productSizeService,
            ICouponCodeService couponCodeService,
            UserManager<ApplicationUser> userManager,
            ISessionService sessionService,
            IMapper mapper)
        {
            this.productsService = productsService;
            this.addressesService = addressesService;
            this.productSizeService = productSizeService;
            this.couponCodeService = couponCodeService;
            this.userManager = userManager;
            this.sessionService = sessionService;
            this.mapper = mapper;
        }

        // TODO Alert messages to separate variable and use global constants for css
        public async Task<IActionResult> Index()
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);

            // TODO Remove dummy data
            if (cart == null || cart.Products.Count <= 0)
            {
                this.SetTempCart();
                cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            }

            var cartViewModel = this.mapper.Map<CartViewModel>(cart);

            var productsViewModels = new List<CartProductViewModel>();
            foreach (SessionCartProductDto productDto in cart.Products)
            {
                /* Create view model */
                var productViewModel = await this.productsService.GetById<CartProductViewModel>(productDto.Id);

                /* Get product size */
                var productSize =
                    this.productSizeService.GetProductSize<ProductSizeViewModel>(productDto.Id, productDto.SizeName);
                productViewModel.Size = productSize;

                /* Map data (Quantity, Size) from Product DTO */
                productViewModel = this.mapper.Map(productDto, productViewModel);

                productsViewModels.Add(productViewModel);
            }

            /* Update the Products View Models in the Cart View Model */
            cartViewModel.Products = productsViewModels;

            /* Get user address */
            var userId = this.userManager.GetUserId(this.User);
            var addressViewModel =
                this.addressesService.GetByUserId<CartAddressViewInputModel>(userId) ?? new CartAddressViewInputModel();
            cartViewModel.Address = addressViewModel;

            /* Get Coupon Code (Discount) */
            var sessionCouponCode =
                this.sessionService.Get<SessionCouponCodeDto>(this.HttpContext.Session, "CouponCode");
            if (sessionCouponCode != null)
            {
                var couponCode = this.couponCodeService.GetBaseByCode(sessionCouponCode.Code);
                if (couponCode != null)
                {
                    cartViewModel.CouponCode = couponCode.Code;
                    cartViewModel.DiscountPercent = couponCode.DiscountPercent;
                }
            }

            return this.View(cartViewModel);
        }

        // Redirect to Menu page in case user is logged in after trying to add item to cart
        [HttpGet]
        public async Task<ActionResult> AddItem()
        {
            return RedirectToAction("Index", "Categories");
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(ProductViewInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                // TODO Move message to constant
                this.TempData["Message"] = "Could not complete your request.";
                this.TempData["MessageType"] = AlertMessageTypes.Error;
                return this.RedirectToAction("View", $"Products", new {id = inputModel.Id});
            }

            var cart = new SessionCartDto();
            if (this.sessionService.TryGet(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY))
            {
                cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            }

            var cartProductModel = this.mapper.Map<SessionCartProductDto>(inputModel);
            cart.Products.Add(cartProductModel);
            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);
            // TODO Move message to constant
            this.TempData["Message"] = "Product added to cart";
            this.TempData["MessageType"] = AlertMessageTypes.Success;
            return this.RedirectToAction("View", $"Products", new
            {
                id = inputModel.Id
            });
        }

        // [HttpPost]
        public async Task<ActionResult> RemoveItem(int itemId, string itemSize)
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            cart.Products
                .RemoveAll(p => p.Id == itemId && p.SizeName == itemSize);

            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);

            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> IncreaseItemQuantity(int itemId, string itemSize)
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            cart.Products
                .FindAll(p => p.Id == itemId && p.SizeName == itemSize && p.Quantity < 10)
                .ForEach(p => p.Quantity++);

            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);

            return this.RedirectToAction("Index");
        }

        public async Task<ActionResult> DecreaseItemQuantity(int itemId, string itemSize)
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            cart.Products
                .FindAll(p => p.Id == itemId && p.SizeName == itemSize && p.Quantity > 1)
                .ForEach(p => p.Quantity--);

            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);

            return this.RedirectToAction("Index");
        }

        public void SetTempCart()
        {
            var cartData = new SessionCartDto()
            {
                Products = new List<SessionCartProductDto>()
                {
                    new SessionCartProductDto()
                    {
                        Id = 2,
                        Quantity = 1,
                        SizeName = "Small",
                    },
                    new SessionCartProductDto()
                    {
                        Id = 3,
                        Quantity = 2,
                        SizeName = "Medium",
                    },
                },
            };
            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cartData);
        }
    }
}
