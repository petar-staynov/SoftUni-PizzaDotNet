namespace PizzaDotNet.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data;
    using PizzaDotNet.Web.ViewModels.Ratings;

    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet]
        // [Route("api/[controller]/{id}")]
        public async Task<RatingResponseModel> GetRating(int productId)
        {
            var productRating = this.ratingsService.GetProductRating(productId);

            var response = new RatingResponseModel
            {
                Rating = productRating,
            };

            return response;
        }

        // [Authorize] // TODO Enable this
        [HttpPost]
        public async Task<IActionResult> Post(RatingInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.ratingsService.RateProductAsync(inputModel.ProductId, userId, inputModel.Value);
            return this.Ok();

            // TODO maybe return product average rating
        }
    }
}
