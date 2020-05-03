namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface ICouponCodeService
    {
        Task<CouponCode> CreateAsync(CouponCode couponCode);

        Task<CouponCode> GetBaseByCode(string codeString);

        Task<CouponCode> CanUseCodeByCodeString(string codeString);

        void UseCodeByCodeString(string code);

        Task<CouponCode> GenerateCouponCodeForUser(int discountPercent, string userId);
    }
}
