namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminProductRatingViewModel : IMapFrom<Rating>
    {
        public int Value { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
