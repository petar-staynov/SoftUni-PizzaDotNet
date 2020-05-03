using PizzaDotNet.Common;

namespace PizzaDotNet.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;

    public class CouponCodeService : ICouponCodeService
    {
        private readonly IRepository<CouponCode> couponCodeRepository;

        public CouponCodeService(IRepository<CouponCode> couponCodeRepository)
        {
            this.couponCodeRepository = couponCodeRepository;
        }

        public async Task<CouponCode> CreateAsync(CouponCode couponCode)
        {
            await this.couponCodeRepository.AddAsync(couponCode);
            await this.couponCodeRepository.SaveChangesAsync();
            return couponCode;
        }

        public async Task<CouponCode> GetBaseByCode(string codeString)
        {
            var code = this.couponCodeRepository
                .All()
                .FirstOrDefault(c => c.Code == codeString);

            return code;
        }

        public async Task<CouponCode> CanUseCodeByCodeString(string codeString)
        {
            var code = await this.GetBaseByCode(codeString);
            if (code == null)
            {
                return null;
            }

            if (code.IsUsed || code.ValidUntil <= DateTime.Now)
            {
                return null;
            }

            return code;
        }

        public async void UseCodeByCodeString(string codeString)
        {
            var code = await this.GetBaseByCode(codeString);
            code.IsUsed = true;

            this.couponCodeRepository.Update(code);
            await this.couponCodeRepository.SaveChangesAsync();
        }

        public async Task<CouponCode> GenerateCouponCodeForUser(int discountPercent, string userId)
        {
            Random random = new Random();
            var codeString = StringGenerator.RandomString(GlobalConstants.CouponCodeStringLength);

            var couponCode = new CouponCode
            {
                UserId = userId,
                Code = codeString,
                DiscountPercent = discountPercent,
                ValidUntil = DateTime.Now.AddMonths(12),
            };

            await this.CreateAsync(couponCode);

            return couponCode;
        }
    }
}
