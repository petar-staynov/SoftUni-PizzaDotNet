namespace PizzaDotNet.Web.ViewModels.Products
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public double? Rating { get; set; }

        public string RatingString => this.Rating != null ? this.Rating.ToString() : "Not Rated";
    }
}
