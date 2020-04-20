namespace PizzaDotNet.Data.EntityData
{
    using System;
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;

    public static class CategoriesData
    {
        public static IEnumerable<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category()
                {
                    Name = "Starters",
                    Description = "Some delicious meal starters.",
                    Products = ProductsData.GetStarters(),
                },
                new Category()
                {
                    Name = "Pizza",
                    Description = "Delicious pizzas made with high quality organic products and care.",
                    Products = ProductsData.GetPizzas(),
                },
                new Category()
                {
                    Name = "Dips",
                    Description = "Carefully selected sauces for your favourite pizzas.",
                    Products = ProductsData.GetDips(),
                },
                new Category()
                {
                    Name = "Drinks",
                    Description = "none",
                    Products = ProductsData.GetDrinks(),
                },
                new Category()
                {
                    Name = "Desserts",
                    Description = "none",
                    Products = ProductsData.GetDesserts(),
                },
            };
        }
    }
}
