namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using PizzaDotNet.Data.EntityData;
    using PizzaDotNet.Data.Models;

    internal class CategoriesAndProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categoriesAndProductsData = CategoriesData.GetCategories();
            await dbContext.Categories.AddRangeAsync(categoriesAndProductsData);
        }
    }
}
