namespace PizzaDotNet.Web.ViewModels.Cart
{
    using System.Collections.Generic;
    using System.Linq;

    public class CartViewModel
    {
        public CartViewModel()
        {
            this.Products = new List<CartProductViewModel>();
        }

        public ICollection<CartProductViewModel> Products { get; set; }

        public decimal? TotalPrice => this.Products.Select(p => p.Price * p.Quantity).Sum();

        public CartAddressViewInputModel Address { get; set; }
    }
}
