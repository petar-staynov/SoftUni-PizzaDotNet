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
        public int Id { get; set; }

        public string UserId { get; set; }

        public OrderAddressViewModel OrderAddress { get; set; }


        public int OrderStatusId { get; set; }

        public OrderStatusViewModel OrderStatus { get; set; }


        public ICollection<OrderProductViewModel> OrderProducts { get; set; }


        public int? CouponCodeId { get; set; }

        public virtual CouponCode CouponCode { get; set; }


        public decimal? TotalPrice { get; set; }

        public decimal? TotalPriceDiscounted { get; set; }


        public string OrderNotes { get; set; }
    }
}
