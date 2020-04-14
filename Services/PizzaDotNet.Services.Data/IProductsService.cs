namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Models;

    public interface IProductsService
    {
        Task<Product> CreateAsync(string name, string description, int categoryId, string imageUrl, string imageStorageName);

        // TODO Add EDIT

        // TODO Add DELETE
        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId);

        IEnumerable<T> GetByCategoryName<T>(string categoryName);
    }
}
