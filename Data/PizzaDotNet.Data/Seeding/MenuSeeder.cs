namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.EntityData;
    using PizzaDotNet.Data.Models;

    public class MenuSeeder : ISeeder
    {
        public Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var categoriesAndProductsData = CategoriesData.GetCategories();
            dbContext.Categories.AddRangeAsync(categoriesAndProductsData);
            return Task.CompletedTask;
        }
    }
}
