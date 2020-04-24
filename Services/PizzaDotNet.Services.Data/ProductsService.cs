namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
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

        public async Task<Product> CreateAsync(string name, string description, int categoryId, List<ProductSize> sizes, string imageUrl, string imageStorageName)
        {
            Product product = new Product
            {
                Name = name,
                Description = description,
                CategoryId = categoryId,
                Sizes = sizes,
                ImageUrl = imageUrl,
                ImageStorageName = imageStorageName,
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

        public IEnumerable<T> GetByCategoryId<T>(int categoryId)
        {
            var query = this.productsRepository
                .All()
                .Where(p => p.CategoryId == categoryId);

            var categoryProducts = query.To<T>().ToList();

            return categoryProducts;
        }

        public IEnumerable<T> GetByCategoryName<T>(string categoryName)
        {
            var query = this.productsRepository
                .All()
                .Where(p => p.Category.Name == categoryName);

            var categoryProducts = query.To<T>().ToList();;

            return categoryProducts;
        }
    }
}
