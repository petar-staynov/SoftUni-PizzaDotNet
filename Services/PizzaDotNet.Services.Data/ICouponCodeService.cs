namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface ICouponCodeService
    {
        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> GetById<T>(int id);

        Task<CouponCode> GetBaseById(int id);

        Task<T> GetByCode<T>(string codeString);

        Task<CouponCode> GetBaseByCode(string codeString);

        Task<CouponCode> CanUseCodeById(int codeId);

        Task<CouponCode> CanUseCodeByCode(string codeString);

        void UseCodeByCode(string code);
    }
}
