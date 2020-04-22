using PizzaDotNet.Web.ViewModels.Cart;

namespace PizzaDotNet.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.DTO;
    using PizzaDotNet.Services;
    using PizzaDotNet.Web.ViewModels.Products;

    public class CartController : BaseController
    {
        private readonly ISessionService sessionService;
        private readonly IMapper mapper;

        public CartController(
            ISessionService sessionService,
            IMapper mapper)
        {
            this.sessionService = sessionService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            //TODO find wether to use dto or view models
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, "Cart");
            var viewModel =this.mapper.Map<CartViewModel>(cart);
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
