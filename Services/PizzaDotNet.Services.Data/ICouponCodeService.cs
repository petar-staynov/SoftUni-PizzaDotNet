namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface ICouponCodeService
    {
        Task<CouponCode> GetBaseByCode(string codeString);

        Task<CouponCode> CanUseCodeByCode(string codeString);

        void UseCodeByCode(string code);
    }
}
