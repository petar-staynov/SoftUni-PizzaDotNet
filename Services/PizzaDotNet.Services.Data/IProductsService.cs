namespace PizzaDotNet.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Models;

    public interface IProductsService
    {
        Task<Product> CreateAsync(string name, string description, decimal price, int categoryId, string imageUrl, IFormFile imageFile);

        T GetById<T>(int id);
    }
}
