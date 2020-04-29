namespace PizzaDotNet.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Orders;

    public class AdminOrderViewModel : IMapFrom<Order>
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }


        public virtual OrderAddress OrderAddress { get; set; }


        public int OrderStatusId { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }


        public ICollection<AdminOrderProductViewModel> OrderProducts { get; set; }


        public int? CouponCodeId { get; set; }

        public virtual CouponCode CouponCode { get; set; }

        public decimal? TotalPrice { get; set; }

        public decimal? TotalPriceDiscounted { get; set; }

        public string OrderNotes { get; set; }
    }
}
