namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Models;

    public interface IProductsService
    {
        int GetCount();

        Task<Product> CreateAsync(string name, string description, int categoryId, List<ProductSize> sizes, string imageUrl, string imageStorageName);

        // TODO Add EDIT

        // TODO Add DELETE
        T GetById<T>(int id);

        Product GetBaseById(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId);

        IEnumerable<T> GetByCategoryName<T>(string categoryName);
    }
}
