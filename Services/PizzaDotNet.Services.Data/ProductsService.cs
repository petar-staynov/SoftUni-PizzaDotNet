namespace PizzaDotNet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<Product> CreateAsync(string name, string description, decimal price, int categoryId, string imageUrl, IFormFile imageFile)
        {
            Product product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                CategoryId = categoryId,
                ImageUrl = imageUrl,
                ImageFile = imageFile,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product;
        }

        public T GetById<T>(int id)
        {
            var product = this.productsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return product;
        }
    }
}
