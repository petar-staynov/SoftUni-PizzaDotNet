namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using PizzaDotNet.Data.EntityData;

    internal class IngredientsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Ingredients.Any())
            {
                return;
            }

            var ingredients = IngredientsData.GetIngredients();
            await dbContext.Ingredients.AddRangeAsync(ingredients);
        }
    }
}
