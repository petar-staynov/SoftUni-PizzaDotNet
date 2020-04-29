namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface IAddressesService
    {
        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> GetByUserId<T>(string id);

        Task<UserAddress> GetBaseByUserId(string id);

        Task<T> GetById<T>(int id);

        Task<UserAddress> GetBaseById(int id);

        Task<UserAddress> CreateAsync(UserAddress address);

        Task<UserAddress> UpdateAsync(UserAddress address);
    }
}
