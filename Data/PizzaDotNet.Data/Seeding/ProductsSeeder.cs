namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Products.Any())
            {
                var products = new List<Product>
                {
                    // new Product { Name = "A", Description = "A", Price = 0M, ImageUrl = "image.jpg", CategoryId = 1 },
                };

                foreach (Product product in products)
                {
                    await dbContext.Products.AddRangeAsync(products);
                }
            }
        }
    }
}