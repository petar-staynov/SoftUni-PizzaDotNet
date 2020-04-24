namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductSizeService : IProductSizeService
    {
        private readonly IDeletableEntityRepository<ProductSize> productSizeRepository;

        public ProductSizeService(IDeletableEntityRepository<ProductSize> productSizeRepository)
        {
            this.productSizeRepository = productSizeRepository;
        }


        public async Task<ProductSize> CreateAsync(int productId, string size, decimal price)
        {
            var productSize = new ProductSize
            {
                ProductId = productId,
                Size = size,
                Price = price,
            };

            await this.productSizeRepository.AddAsync(productSize);
            await this.productSizeRepository.SaveChangesAsync();

            return productSize;
        }

        public ICollection<T> GetByProductId<T>(int productId)
        {
            var query = this.productSizeRepository
                .All()
                .Where(x => x.ProductId == productId);

            var productSizes = query.To<T>().ToList();

            return productSizes;
        }

        public decimal GetSizePrice(int productId, string size)
        {
            ProductSize productSize = this.productSizeRepository
                .All()
                .FirstOrDefault(s => s.ProductId == productId && s.Size == size);

            if (productSize == null)
            {
                // TODO Handle this error
            }

            return productSize.Price;
        }
    }
}
