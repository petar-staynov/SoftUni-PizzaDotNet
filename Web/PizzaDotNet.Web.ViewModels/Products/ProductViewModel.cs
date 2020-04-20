namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.SizeOfProduct;

    public class ProductViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public ICollection<SizeOfProductViewModel> Sizes { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public double? Rating { get; set; }

        public string RatingString => this.Rating != null ? this.Rating.ToString() : "Not Rated";
    }
}
