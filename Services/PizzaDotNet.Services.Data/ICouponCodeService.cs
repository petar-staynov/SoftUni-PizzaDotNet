namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using PizzaDotNet.Data.Models;

    public interface ICouponCodeService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        CouponCode GetBaseById(int id);

        T GetByCode<T>(string codeString);

        CouponCode GetBaseByCode(string codeString);

        CouponCode CanUseCodeById(int codeId);

        CouponCode CanUseCodeByCode(string codeString);

        void UseCodeByCode(string code);
    }
}
