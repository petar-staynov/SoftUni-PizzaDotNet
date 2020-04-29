namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Models;

    public interface IProductsService
    {
        Task<int> GetCount();

        Task<Product> CreateAsync(Product product);

        Task<Product> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int productId);

        Task<T> GetById<T>(int id);

        Task<Product> GetBaseById(int id);

        Task<IEnumerable<T>> GetByCategoryId<T>(int categoryId, string sortCriteria = null, int? count = null);

        Task<IEnumerable<T>> GetAll<T>(string sortCriteria = null, int? count = null);
    }
}
