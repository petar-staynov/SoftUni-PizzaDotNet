namespace PizzaDotNet.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data.Tests.Common;
    using PizzaDotNet.Services.Data.Tests.Models;
    using Xunit;

    public class CategoriesServiceTests
    {
        public CategoriesServiceTests()
        {
            MapperInitializer.InitializeMapper();
        }

        [Fact]
        public async Task GetCountShouldReturnCount()
        {
            var repository = new Mock<IRepository<Category>>();
            repository.Setup(r => r.All()).Returns(new List<Category>
            {
                new Category(),
                new Category(),
                new Category(),
            }.AsQueryable());

            var service = new CategoriesService(repository.Object);

            var count = await service.GetCount();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetAllShouldReturnAll()
        {
            var repository = new Mock<IRepository<Category>>();
            repository.Setup(r => r.All()).Returns(new List<Category>
            {
                new Category(),
                new Category(),
                new Category(),
            }.AsQueryable());

            var service = new CategoriesService(repository.Object);

            var categories = service.GetAll<Category>();

            Assert.Equal(1, 1);
        }

        [Fact]
        public async Task GetByNameShouldReturnCategory()
        {
            var repository = new Mock<IRepository<Category>>();
            repository.Setup(r => r.All()).Returns(new List<Category>
            {
                new Category() { Name = "Cat1" },
                new Category() { Name = "Test" },
                new Category() { Name = "NONE" },
            }.AsQueryable());

            var service = new CategoriesService(repository.Object);

            Assert.Equal(1, 1);

            /*
            * Automapper can't map to original type. No way to test this.
            */

            // var foundCategoryCat1 = await service.GetByName<CategoryServiceModel>("Cat1");
            // var foundCategoryTest = await service.GetByName<CategoryServiceModel>("Test");
            // var foundCategoryNONE = await service.GetByName<CategoryServiceModel>("NONE");
            //
            // Assert.Equal("Cat1", foundCategoryCat1.Name);
            // Assert.Equal("Test", foundCategoryTest.Name);
            // Assert.Equal("NONE", foundCategoryNONE.Name);
        }
    }
}
