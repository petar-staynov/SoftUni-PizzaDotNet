namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class SizesOfProductService : ISizesOfProductService
    {
        private readonly IDeletableEntityRepository<SizeOfProduct> sizeOfProductRepository;

        public SizesOfProductService(IDeletableEntityRepository<SizeOfProduct> sizeOfProductRepository)
        {
            this.sizeOfProductRepository = sizeOfProductRepository;
        }


        public async Task<SizeOfProduct> CreateAsync(int productId, string size, decimal price)
        {
            var sizeOfProduct = new SizeOfProduct
            {
                ProductId = productId,
                Size = size,
                Price = price,
            };

            await this.sizeOfProductRepository.AddAsync(sizeOfProduct);
            await this.sizeOfProductRepository.SaveChangesAsync();

            return sizeOfProduct;
        }

        public ICollection<T> GetByProductId<T>(int productId)
        {
            var query = this.sizeOfProductRepository
                .All()
                .Where(x => x.ProductId == productId);

            var productSizes = query.To<T>().ToList();

            return productSizes;
        }
    }
}
