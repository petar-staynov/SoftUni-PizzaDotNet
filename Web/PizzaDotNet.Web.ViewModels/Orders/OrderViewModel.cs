namespace PizzaDotNet.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.Linq;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Addresses;
    using PizzaDotNet.Web.ViewModels.Orders;
    using PizzaDotNet.Web.ViewModels.Products;

    public class OrderViewModel : IMapFrom<Order>
    {
        public OrderAddressViewModel OrderAddress { get; set; }


        public int OrderStatusId { get; set; }

        public OrderStatusViewModel OrderStatus { get; set; }


        public ICollection<OrderProductViewModel> OrderProducts { get; set; }


        public int? CouponCodeId { get; set; }

        public virtual CouponCode CouponCode { get; set; }


        public decimal? TotalPrice =>
            this.OrderProducts.Select(p => p.TotalPrice).Sum();

        public decimal? TotalPriceDiscounted => this.CouponCodeId != null
            ? this.TotalPrice * (decimal?) (1 - (this.CouponCode.DiscountPercent / 100))
            : this.TotalPrice;


        public string OrderNotes { get; set; }
    }
}
