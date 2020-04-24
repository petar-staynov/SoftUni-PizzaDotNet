namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface IAddressesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByUserId<T>(string id);

        T GetById<T>(int id);

        Task<UserAddress> CreateAddressAsync(UserAddress address);

        Task<UserAddress> UpdateAddressAsync(UserAddress address);
    }
}
