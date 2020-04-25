using System.ComponentModel.DataAnnotations;

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

        public int DiscountPercent { get; set; }

        public decimal? TotalPrice => this.Products.Select(p => p.Price * p.Quantity).Sum();

        public decimal? DiscountPrice => this.TotalPrice * (1 - (this.DiscountPercent / 100));

        public CartAddressViewInputModel Address { get; set; }

        [Display(Name = "Additional notes:")]
        public string AdditionalNotes { get; set; }

        [Display(Name = "Use this as my default address")]
        public bool UseAddress { get; set; }
    }
}
