namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminProductRatingViewModel : IMapFrom<Rating>
    {
        public int Value { get; set; }
    }
}
