using System.ComponentModel;
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
            this.DiscountPercent = 0;
        }

        public string UserId { get; set; }

        public List<CartProductViewModel> Products { get; set; }

        public decimal? TotalPrice => this.Products.Select(p => p.Price * p.Quantity).Sum();

        [StringLength(6, MinimumLength = 6)]
        [DisplayName("Coupon code...")]
        public string CouponCode { get; set; }

        public float DiscountPercent { get; set; }

        public decimal? DiscountPrice => this.DiscountPercent > 0
            ? this.TotalPrice * (decimal?)(1 - (this.DiscountPercent / 100))
            : this.TotalPrice;

        public CartAddressViewInputModel Address { get; set; }

        [Display(Name = "Additional notes:")]
        public string AdditionalNotes { get; set; }

        [Display(Name = "Use this as my default address")]
        public bool UseAddress { get; set; }
    }
}
