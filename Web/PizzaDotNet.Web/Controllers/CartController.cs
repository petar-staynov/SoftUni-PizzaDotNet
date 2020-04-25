namespace PizzaDotNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
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

    public class CartController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IAddressesService addressesService;
        private readonly IProductSizeService productSizeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISessionService sessionService;
        private readonly IMapper mapper;

        public CartController(
            IProductsService productsService,
            IAddressesService addressesService,
            IProductSizeService productSizeService,
            UserManager<ApplicationUser> userManager,
            ISessionService sessionService,
            IMapper mapper)
        {
            this.productsService = productsService;
            this.addressesService = addressesService;
            this.productSizeService = productSizeService;
            this.userManager = userManager;
            this.sessionService = sessionService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, "Cart");

            // TODO Remove dummy data
            if (cart == null || cart.Products.Count <= 0)
            {
                this.SetTempCart();
                cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, "Cart");
            }

            var cartViewModel = this.mapper.Map<CartViewModel>(cart);

            var productsViewModels = new List<CartProductViewModel>();
            foreach (SessionCartProductDto productDto in cart.Products)
            {
                /* Create view model */
                var productViewModel = this.productsService.GetById<CartProductViewModel>(productDto.Id);

                /* Get product size */
                var productSize =
                    this.productSizeService.GetProductSize<ProductSizeViewModel>(productDto.Id, productDto.SizeString);
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

            return this.View(cartViewModel);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddItem(ProductViewInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["Message"] = "Could not complete your request.";
                this.TempData["MessageType"] = "danger";
                return this.RedirectToAction("ViewById", $"Products", new {id = inputModel.Id});
            }

            var cart = new SessionCartDto();
            if (this.sessionService.TryGet(this.HttpContext.Session, "Cart"))
            {
                cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, "Cart");
            }

            var cartProductModel = this.mapper.Map<SessionCartProductDto>(inputModel);
            cart.Products.Add(cartProductModel);
            this.sessionService.Set(this.HttpContext.Session, "Cart", cart);
            this.TempData["Message"] = "Product added to cart";
            this.TempData["MessageType"] = "success";
            return this.RedirectToAction("ViewById", $"Products", new
            {
                id = inputModel.Id
            });
        }

        // [HttpPost]
        public async Task<ActionResult> RemoveItem(int itemId)
        {
            return this.Ok();
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
                        SizeString = "Small",
                    },
                    new SessionCartProductDto()
                    {
                        Id = 3,
                        Quantity = 2,
                        SizeString = "Medium",
                    },
                },
            };
            this.sessionService.Set(this.HttpContext.Session, "Cart", cartData);
        }
    }
}