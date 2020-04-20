namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.EntityData;

    public class IngredientsSeeder : ISeeder
    {
        public Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var ingredients = IngredientsData.GetIngredients();
            dbContext.Ingredients.AddRangeAsync(ingredients);
            return Task.CompletedTask;
        }
    }
}
