namespace PizzaDotNet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CouponCodeService : ICouponCodeService
    {
        private readonly IRepository<CouponCode> couponCodeRepository;

        public CouponCodeService(IRepository<CouponCode> couponCodeRepository)
        {
            this.couponCodeRepository = couponCodeRepository;
        }

        public async Task<CouponCode> GetBaseByCode(string codeString)
        {
            var code = await this.couponCodeRepository
                .All()
                .FirstOrDefaultAsync(c => c.Code == codeString);

            return code;
        }

        public async Task<CouponCode> CanUseCodeByCode(string codeString)
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

        public async void UseCodeByCode(string codeString)
        {
            var code = await this.GetBaseByCode(codeString);
            code.IsUsed = true;

            this.couponCodeRepository.Update(code);
            await this.couponCodeRepository.SaveChangesAsync();
        }
    }
}
