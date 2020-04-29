namespace PizzaDotNet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<T> GetProductSize<T>(int productId, string sizeString)
        {
            var query = this.productSizeRepository
                .All()
                .Where(s => s.ProductId == productId && s.Name == sizeString);

            var productSize = await query.To<T>().FirstOrDefaultAsync();

            return productSize;
        }

        public async Task<ProductSize> GetProductSizeBase(int productId, string sizeString)
        {
            var productSize = await this.productSizeRepository
                .All()
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.Name == sizeString);

            return productSize;
        }

        public async Task<bool> DeleteProductSizes(int productId)
        {
            var sizes = await this.productSizeRepository
                .All()
                .Where(s => s.ProductId == productId)
                .ToListAsync();

            foreach (var productSize in sizes)
            {
                this.productSizeRepository.Delete(productSize);
            }

            int result = await this.productSizeRepository.SaveChangesAsync();
            return result > 0;
        }
    }
}
