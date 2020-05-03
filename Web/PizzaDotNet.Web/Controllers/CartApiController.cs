namespace PizzaDotNet.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Common;
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
        public CartItemsNumberResponseModel GetNumberOfCartItems()
        {
            var cart = this.sessionService.Get<SessionCartDto>(this.HttpContext.Session, GlobalConstants.SessionCartKey);
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
