namespace PizzaDotNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Products;

    public class CartController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly ISessionService sessionService;
        private readonly ISizesOfProductService sizesOfProductService;
        private readonly IMapper mapper;

        public CartController(
            IProductsService productsService,
            ISessionService sessionService,
            ISizesOfProductService sizesOfProductService,
            IMapper mapper)
        {
            this.productsService = productsService;
            this.sizesOfProductService = sizesOfProductService;
            this.sessionService = sessionService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, "Cart");
            if (cart == null)
            {
                cart = new SessionCartDto()
                {
                    Products = new List<SessionCartProductDto>()
                    {
                        new SessionCartProductDto()
                        {
                            Id = 2,
                            Quantity = 1,
                            Size = "Small",
                        },
                        new SessionCartProductDto()
                        {
                            Id = 3,
                            Quantity = 2,
                            Size = "Medium",
                        },
                    },
                };
            }

            var viewModel = this.mapper.Map<CartViewModel>(cart);

            List<CartProductViewModel> productsViewModels = new List<CartProductViewModel>();
            foreach (SessionCartProductDto productDto in cart.Products)
            {
                /* Create view model */
                var productViewModel = this.productsService.GetById<CartProductViewModel>(productDto.Id);

                /* Get product size */
                var productPrice = this.sizesOfProductService.GetSizePrice(productDto.Id, productDto.Size);

                /* Map data (Quantity, Size) from Product DTO to Product View Model
                 * This can not be auto-mapped */
                productViewModel.Quantity = productDto.Quantity;
                productViewModel.Size = productDto.Size;
                productViewModel.Price = productPrice;

                productsViewModels.Add(productViewModel);
            }

            /* Update the Products View Models in the Cart View Model */
            viewModel.Products = productsViewModels;

            return this.View(viewModel);
        }

        // [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddItem(ProductViewInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["Message"] = "Could not complete your request.";
                this.TempData["MessageType"] = "danger";
                return this.RedirectToAction("ViewById", $"Products", new { id=inputModel.Id });
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
            return this.RedirectToAction("ViewById", $"Products", new { id=inputModel.Id });
        }
    }
}
