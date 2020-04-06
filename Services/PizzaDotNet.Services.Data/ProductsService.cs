namespace PizzaDotNet.Services.Data
{
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> CreateAsync(string name, string description, decimal price, string imageUrl, int categoryId)
        {
            Product product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product.Id;
        }
    }
}
