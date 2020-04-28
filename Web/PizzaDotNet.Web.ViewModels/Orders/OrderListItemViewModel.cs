using System.Collections.Generic;
using System.Linq;

namespace PizzaDotNet.Web.ViewModels.Orders
{
    using System;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class OrderListItemViewModel : IMapFrom<Order>
    {
        private DateTime createdOn;
        public int Id { get; set; }

        public DateTime CreatedOn
        {
            get => this.createdOn.Date;
            set => this.createdOn = value;
        }

        public virtual OrderStatus OrderStatus { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public decimal? TotalPrice => this.OrderProducts.Select(p => p.Price * p.Quantity).Sum();


        public int? CouponCodeId { get; set; }

        public virtual CouponCode CouponCode { get; set; }

        public decimal? TotalPriceDiscounted => this.CouponCode != null
            ? this.TotalPrice * (decimal?)(1 - (this.CouponCode.DiscountPercent / 100))
            : this.TotalPrice;
    }
}
