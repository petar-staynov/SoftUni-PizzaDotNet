namespace PizzaDotNet.Web.ViewModels.Cart
{
    using System;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.ProductSize;

    public class CartProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProductSizeViewModel Size { get; set; }

        public string SizeString => this.Size.Size;

        public int Quantity { get; set; }

        public decimal? Price => Math.Round(this.Size.Price);

        public decimal? TotalPrice => this.Price * this.Quantity;

        public string ImageUrl { get; set; }
    }
}
