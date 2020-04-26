namespace PizzaDotNet.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Common.Models;

    public class CouponCode : BaseDeletableModel<int>
    {
        [StringLength(6, MinimumLength = 6)]
        public string Code { get; set; }

        [Range(0, 100)]
        public int DiscountPercent { get; set; }

        public DateTime ValidUntil { get; set; }

        public bool IsUsed { get; set; }
    }
}
