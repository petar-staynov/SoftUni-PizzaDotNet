namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Models;

    public interface IProductsService
    {
        int GetCount();

        Task<Product> CreateAsync(Product product);

        Task<Product> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int orderId);

        T GetById<T>(int id);

        Product GetBaseById(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, string sortCriteria = null, int? count = null);

        IEnumerable<T> GetAll<T>(string sortCriteria = null, int? count = null);
    }
}
