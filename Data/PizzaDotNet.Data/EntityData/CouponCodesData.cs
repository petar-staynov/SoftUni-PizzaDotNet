namespace PizzaDotNet.Data.EntityData
{
    using System;
    using System.Collections.Generic;

    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Models;

    public class CouponCodesData
    {
        private static readonly Random random = new Random();

        public static IEnumerable<CouponCode> GetCouponCodes()
        {
            var couponCodeList = new List<CouponCode>();
            for (int i = 0; i < 100; i++)
            {
                var codeString = StringGenerator.RandomString(6);

                var code = new CouponCode
                {
                    Code = codeString,
                    DiscountPercent = random.Next(1, 50),
                    ValidUntil = DateTime.Now.AddMonths(12),
                };

                couponCodeList.Add(code);
            }

            return couponCodeList;
        }
    }
}
