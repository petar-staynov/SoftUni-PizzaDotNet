﻿namespace PizzaDotNet.Data.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using PizzaDotNet.Data.Common.Models;

    public class Order : BaseModel<int>
    {
        public Order()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
        }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


        public OrderAddress Address { get; set; }


        public int OrderStatusId { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }

        public decimal? TotalPrice => this.OrderProducts.Select(p => p.Price * p.Quantity).Sum();

        public int CouponCodeId { get; set; }

        public virtual CouponCode CouponCode { get; set; }

        public string CouponCodeString { get; set; }

        public float DiscountPercent { get; set; }

        public decimal? TotalPriceDiscounted => this.DiscountPercent > 0
        ? this.TotalPrice * (decimal?)(1 - (this.DiscountPercent / 100))
        : this.TotalPrice;

        public string OrderNotes { get; set; }
    }
}
