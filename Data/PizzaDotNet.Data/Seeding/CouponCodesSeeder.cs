namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using PizzaDotNet.Data.EntityData;

    /*
     * NO LONGER IN USE AS COUPON CODES ARE GENERATED INDIVIDUALLY FOR EACH USER
     */
    public class CouponCodesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.CouponCodes.Any())
            {
                return;
            }

            var couponCodesData = CouponCodesData.GetCouponCodes();
            await dbContext.CouponCodes.AddRangeAsync(couponCodesData);
        }
    }
}
