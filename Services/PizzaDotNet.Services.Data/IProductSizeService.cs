namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface IProductSizeService
    {
        Task<ProductSize> CreateAsync(int productId, string size, decimal price);

        ICollection<T> GetAllProductSizes<T>(int productId);

        public T GetProductSize<T>(int productId, string sizeString);

        public ProductSize GetProductSizeBase(int productId, string sizeString);
    }
}
