namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface IAddressesService
    {
        Task<T> GetByUserId<T>(string id);

        Task<UserAddress> GetBaseByUserId(string id);

        Task<UserAddress> CreateAsync(UserAddress address);

        Task<UserAddress> UpdateAsync(UserAddress address);
    }
}
