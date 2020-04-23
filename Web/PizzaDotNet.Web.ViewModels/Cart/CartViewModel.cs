namespace PizzaDotNet.Web.ViewModels.Cart
{
    using System.Collections.Generic;
    using System.Linq;

    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Products;

    public class CartViewModel
    {
        private decimal totalPrice;

        public CartViewModel()
        {
            this.Products = new List<CartProductViewModel>();
        }

        public ICollection<CartProductViewModel> Products { get; set; }

        public decimal TotalPrice
        {
            get => this.Products.Select(p => p.Price * p.Quantity).Sum();
            set => this.totalPrice = value;
        }
    }
}
