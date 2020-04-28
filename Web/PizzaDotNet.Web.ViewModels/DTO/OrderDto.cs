namespace PizzaDotNet.Web.ViewModels.DTO
{
    using System.Collections.Generic;
    using System.Linq;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual OrderAddress OrderAddress { get; set; }

        public int OrderStatusId { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public decimal? TotalPrice { get; set; }

        public int? CouponCodeId { get; set; }

        public virtual CouponCode CouponCode { get; set; }

        public decimal? TotalPriceDiscounted { get; set; }

        public string OrderNotes { get; set; }
    }
}
