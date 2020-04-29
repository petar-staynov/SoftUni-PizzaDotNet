namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface IProductSizeService
    {
        Task<ProductSize> CreateAsync(int productId, string size, decimal price);

        Task<ICollection<T>> GetAllProductSizes<T>(int productId);

        Task<T> GetProductSize<T>(int productId, string sizeString);

        Task<ProductSize> GetProductSizeBase(int productId, string sizeString);

        Task<bool> DeleteProductSizes(int productId);
    }
}
