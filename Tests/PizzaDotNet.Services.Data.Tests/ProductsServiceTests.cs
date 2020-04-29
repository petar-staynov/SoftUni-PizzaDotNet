namespace PizzaDotNet.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Repositories;
    using PizzaDotNet.Services.Data.Tests.Common;
    using Xunit;

    public class ProductsServiceTests
    {
        [Fact]
        public async Task GetCountShouldReturnCount()
        {
            var productsRepository = new Mock<IDeletableEntityRepository<Product>>();
            productsRepository.Setup(r => r.All()).Returns(new List<Product>
            {
                new Product(),
                new Product(),
                new Product(),
            }.AsQueryable());

            var productsService = new ProductsService(productsRepository.Object);

            var count = await productsService.GetCount();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateProduct()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var productsService = new ProductsService(repository);

            var product = new Product()
            {
                Name = "TestProduct",
                Description = "none",
                CategoryId = 3,
                Category = new Category() { Id = 3, Name = "Test Category " },
                ImageUrl = null,
                ImageStorageName = null,
                Sizes = new List<ProductSize>() { new ProductSize() { Id = 1, Name = "S", Price = 5M } },
                Ratings = null,
            };

            await productsService.CreateAsync(product);

            var products = dbContext.Products.ToList();
            var productsCount = products.Count;
            Assert.Equal(1, productsCount);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateProduct()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var productsService = new ProductsService(repository);

            // Create
            var product = new Product()
            {
                Name = "TestProduct",
                Description = "none",
                CategoryId = 3,
                Category = new Category() { Id = 1, Name = "Test Category " },
                ImageUrl = null,
                ImageStorageName = null,
                Ratings = null,
            };
            var product2 = new Product()
            {
                Name = "TestProduct",
                Description = "none",
                CategoryId = 3,
                Category = new Category() { Id = 2, Name = "Category two" },
                ImageUrl = null,
                ImageStorageName = null,
                Ratings = null,
            };
            await productsService.CreateAsync(product);
            await productsService.CreateAsync(product2);

            // Update
            product.Name = "NameEdited";
            product.Description = "some";
            product.CategoryId = 2;
            product.Category = new Category() { Id = 2, Name = "Category two" };
            await productsService.UpdateAsync(product);

            // Get
            var productsFromDb = dbContext.Products.ToList();
            var productFromDb = productsFromDb.First();

            Assert.Equal(product.Id, productFromDb.Id);
            Assert.Equal("NameEdited", productFromDb.Name);
            Assert.Equal(2, productFromDb.CategoryId);
            Assert.Equal("Category two", productFromDb.Category.Name);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteProduct()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var productsService = new ProductsService(repository);

            // Create
            var product = new Product()
            {
                Name = "TestProduct",
                Description = "none",
                CategoryId = 3,
                Category = new Category() { Id = 1, Name = "Test Category " },
                ImageUrl = null,
                ImageStorageName = null,
                Ratings = null,
            };
            await productsService.CreateAsync(product);

            // Delete
            await productsService.DeleteAsync(product.Id);

            // Get
            var productsFromDb = dbContext.Products.ToList();

            Assert.Equal(0, productsFromDb.Count);
        }

        [Fact]
        public async Task GetBaseByIdShouldReturnProduct()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<Product>(dbContext);
            var productsService = new ProductsService(repository);

            // Create
            var product = new Product()
            {
                Name = "TestProduct",
                Description = "none",
                CategoryId = 3,
                Category = new Category() { Id = 1, Name = "Test Category " },
                ImageUrl = null,
                ImageStorageName = null,
                Ratings = null,
            };
            await productsService.CreateAsync(product);

            // Get
            var productsFromDb = await productsService.GetBaseById(product.Id);

            Assert.Equal(product.Id, productsFromDb.Id);
        }

        [Fact]
        public async Task GetByCategoryIdShouldReturnCorrectProducts()
        {
            var productsRepository = new Mock<IDeletableEntityRepository<Product>>();
            productsRepository.Setup(r => r.All()).Returns(new List<Product>
            {
                new Product() { CategoryId = 1, },
                new Product() { CategoryId = 2, },
                new Product() { CategoryId = 3, },
            }.AsQueryable());

            var productsService = new ProductsService(productsRepository.Object);

            var productsFromCategory =
                await productsService.GetByCategoryId<Product>(1);

            var count = productsFromCategory.Count();
            var firstProduct = productsFromCategory.First();

            Assert.Equal(1, count);
            Assert.Equal(1, firstProduct.CategoryId);
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectProducts()
        {
            var productsRepository = new Mock<IDeletableEntityRepository<Product>>();
            productsRepository.Setup(r => r.All()).Returns(new List<Product>
            {
                new Product() { CategoryId = 1, },
                new Product() { CategoryId = 2, },
                new Product() { CategoryId = 3, },
            }.AsQueryable());

            var productsService = new ProductsService(productsRepository.Object);

            var productsFromCategory =
                await productsService.GetAll<Product>();

            var count = productsFromCategory.Count();
            var firstProduct = productsFromCategory.First();

            Assert.Equal(3, count);
            Assert.Equal(1, firstProduct.CategoryId);
        }
    }
}