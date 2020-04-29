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
                Name = size,
                Price = price,
            };

            await this.productSizeRepository.AddAsync(productSize);
            await this.productSizeRepository.SaveChangesAsync();

            return productSize;
        }

        public ICollection<T> GetAllProductSizes<T>(int productId)
        {
            var query = this.productSizeRepository
                    .All()
                    .Where(s => s.ProductId == productId);

            var productSizes = query.To<T>().ToList();

            return productSizes;
        }

        public T GetProductSize<T>(int productId, string sizeString)
        {
            var query = this.productSizeRepository
                .All()
                .Where(s => s.ProductId == productId && s.Name == sizeString);

            var productSize = query.To<T>().FirstOrDefault();

            return productSize;
        }

        public ProductSize GetProductSizeBase(int productId, string sizeString)
        {
            var productSize = this.productSizeRepository
                .All()
                .FirstOrDefault(s => s.ProductId == productId && s.Name == sizeString);

            return productSize;
        }

        public async Task<bool> DeleteProductSizes(int productId)
        {
            var sizes = this.productSizeRepository
                .All()
                .Where(s => s.ProductId == productId)
                .ToList();

            foreach (var productSize in sizes)
            {
                this.productSizeRepository.Delete(productSize);
            }

            int result = await productSizeRepository.SaveChangesAsync();
            return result > 0;
        }
    }
}
