namespace PizzaDotNet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            var codes = this.couponCodeRepository
                .All()
                .To<T>()
                .ToList();

            return codes;
        }

        public T GetById<T>(int id)
        {
            var code = this.couponCodeRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();

            return code;
        }

        public CouponCode GetBaseById(int id)
        {
            var code = this.couponCodeRepository
                .All()
                .FirstOrDefault(c => c.Id == id);

            return code;
        }

        public T GetByCode<T>(string codeString)
        {
            var code = this.couponCodeRepository
                .All()
                .Where(c => c.Code == codeString)
                .To<T>()
                .FirstOrDefault();

            return code;
        }

        public CouponCode GetBaseByCode(string codeString)
        {
            var code = this.couponCodeRepository
                .All()
                .FirstOrDefault(c => c.Code == codeString);

            return code;
        }

        public CouponCode CanUseCodeById(int codeId)
        {
            var code = this.GetBaseById(codeId);
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

        public CouponCode CanUseCodeByCode(string codeString)
        {
            var code = this.GetBaseByCode(codeString);
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

        public void UseCodeByCode(string codeString)
        {
            var code = this.GetBaseByCode(codeString);
            code.IsUsed = true;

            this.couponCodeRepository.Update(code);
        }
    }
}
