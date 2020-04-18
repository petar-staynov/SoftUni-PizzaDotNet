namespace PizzaDotNet.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Ratings;

    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService ratingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public RatingsController(
            IRatingsService ratingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.ratingsService = ratingsService;
            this.userManager = userManager;
        }

        // GET: api/Ratings
        public IActionResult Get()
        {
            return this.Ok();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<RatingResponseModel> GetRating(int id)
        {
            var productRating = this.ratingsService.GetProductRating(id);

            var response = new RatingResponseModel
            {
                Rating = productRating,
            };

            return response;
        }

        // GET: api/Rating/UserRating/1
        [HttpGet("UserRating/{productId}")]
        public async Task<UserRatingResponseModel> GetUserRating(int productId)
        {
            var userId = this.userManager.GetUserId(this.User);

            var productUserRating = this.ratingsService.GetProductUserRating(productId, userId);

            var response = new UserRatingResponseModel
            {
                Rating = productUserRating,
            };

            return response;
        }

        // [Authorize] // TODO Enable this
        [HttpPost]
        public async Task<IActionResult> RateProduct(RatingInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.ratingsService.RateProductAsync(inputModel.ProductId, userId, inputModel.Value);
            return this.Ok();

            // TODO maybe return product average rating
        }
    }
}
