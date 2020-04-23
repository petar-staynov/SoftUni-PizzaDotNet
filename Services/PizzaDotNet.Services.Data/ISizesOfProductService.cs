namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface ISizesOfProductService
    {
        Task<SizeOfProduct> CreateAsync(int productId, string size, decimal price);

        ICollection<T> GetByProductId<T>(int productId);

        decimal GetSizePrice(int productId, string size);
    }
}
