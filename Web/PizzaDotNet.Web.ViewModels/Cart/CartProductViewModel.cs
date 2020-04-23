namespace PizzaDotNet.Web.ViewModels.Cart
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.SizeOfProduct;

    public class CartProductViewModel : IMapFrom<Product>
    {
        private decimal totalPrice;


        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice
        {
            get => this.Price * this.Quantity;
            set => this.totalPrice = value;
        }

        public string ImageUrl { get; set; }
    }
}
