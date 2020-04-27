﻿using PizzaDotNet.Common;

namespace PizzaDotNet.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Services;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Ratings;

    [Route("api/Cart")]
    [ApiController]
    public class CartApiController : BaseController
    {
        private readonly ISessionService sessionService;

        public CartApiController(
            ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        [HttpGet("NumberOfItems")]
        public async Task<CartItemsNumberResponseModel> GetNumberOfCartItems()
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SESSION_CART_KEY);
            if (cart == null)
            {
                cart = new SessionCartDto();
            }

            int numberOfItems = cart.Products.Count;

            var response = new CartItemsNumberResponseModel()
            {
                NumberOfItems = numberOfItems,
            };

            return response;
        }
    }
}
