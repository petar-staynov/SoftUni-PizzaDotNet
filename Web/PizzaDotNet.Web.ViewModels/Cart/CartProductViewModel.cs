namespace PizzaDotNet.Web.ViewModels.Cart
{
    using System;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.ProductSize;

    public class CartProductViewModel : IMapFrom<Product>
    {
        private decimal totalPrice;
        private decimal price;


        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price
        {
            get => Math.Round(this.price, 2);
            set => this.price = value;
        }

        public decimal TotalPrice
        {
            get => this.Price * this.Quantity;
            set => this.totalPrice = value;
        }

        public string ImageUrl { get; set; }
    }
}
