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
    using PizzaDotNet.Common;
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
        private const string CART_ADD_PRODUCT = "Product added to cart";
        private const string CART_REMOVE_PRODUCT = "Product added to cart";
        private const string CART_INVALID_ITEM = "Invalid item";

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

        public async Task<IActionResult> Index()
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            if (cart == null)
            {
                this.sessionService.Set(
                    this.HttpContext.Session,
                    GlobalConstants.SESSION_CART_KEY,
                    new SessionCartDto());
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
                    await this.productSizeService.GetProductSize<ProductSizeViewModel>(productDto.Id, productDto.SizeName);
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
               await this.addressesService.GetByUserId<CartAddressViewInputModel>(userId) ?? new CartAddressViewInputModel();
            cartViewModel.Address = addressViewModel;

            /* Get Coupon Code (Discount) */
            var sessionCouponCode =
                this.sessionService.Get<SessionCouponCodeDto>(this.HttpContext.Session, GlobalConstants.SESSION_COUPONCODE_KEY);
            if (sessionCouponCode != null)
            {
                var couponCode = await this.couponCodeService.GetBaseByCode(sessionCouponCode.Code);
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
        public IActionResult AddItem()
        {
            return this.RedirectToAction("Index", "Categories");
        }

        [HttpPost]
        public IActionResult AddItem(ProductViewInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["Message"] = CART_INVALID_ITEM;
                this.TempData["MessageType"] = AlertMessageTypes.Error;
                return this.RedirectToAction("View", $"Products", new { id = inputModel.Id });
            }

            var cart = new SessionCartDto();
            if (this.sessionService.TryGet(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY))
            {
                cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            }

            var cartProductModel = this.mapper.Map<SessionCartProductDto>(inputModel);
            cart.Products.Add(cartProductModel);
            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);

            this.TempData["Message"] = CART_ADD_PRODUCT;
            this.TempData["MessageType"] = AlertMessageTypes.Success;
            return this.RedirectToAction("View", $"Products", new
            {
                id = inputModel.Id,
            });
        }

        public IActionResult RemoveItem(int itemId, string itemSize)
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            cart.Products
                .RemoveAll(p => p.Id == itemId && p.SizeName == itemSize);

            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);

            return this.RedirectToAction("Index");
        }

        public IActionResult IncreaseItemQuantity(int itemId, string itemSize)
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            cart.Products
                .FindAll(p => p.Id == itemId && p.SizeName == itemSize && p.Quantity < 10)
                .ForEach(p => p.Quantity++);

            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);

            return this.RedirectToAction("Index");
        }

        public IActionResult DecreaseItemQuantity(int itemId, string itemSize)
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            cart.Products
                .FindAll(p => p.Id == itemId && p.SizeName == itemSize && p.Quantity > 1)
                .ForEach(p => p.Quantity--);

            this.sessionService.Set(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY, cart);

            return this.RedirectToAction("Index");
        }
    }
}
