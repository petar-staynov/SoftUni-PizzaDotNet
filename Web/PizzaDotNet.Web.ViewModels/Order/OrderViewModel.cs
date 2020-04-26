namespace PizzaDotNet.Web.ViewModels.Order
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Web.ViewModels.Addresses;
    using PizzaDotNet.Web.ViewModels.Products;

    public class OrderViewModel
    {
        public string UserId { get; set; }

        public AddressViewInputModel Address { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatusViewModel OrderStatus { get; set; }

        public ICollection<ProductViewInputModel> OrderProducts { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? TotalPriceDiscounted { get; set; }

        public string OrderNotes { get; set; }

        // public int DiscountCodeId { get; set; }

        // public virtual DiscountCode DiscountCode { get; set; }
    }
}
