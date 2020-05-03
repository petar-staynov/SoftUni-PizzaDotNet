namespace PizzaDotNet.Web.ViewModels.CouponCodes
{
    using System;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CouponCodeViewModel : IMapFrom<CouponCode>
    {
        public string Code { get; set; }

        public int DiscountPercent { get; set; }

        public DateTime ValidUntil { get; set; }
    }
}
