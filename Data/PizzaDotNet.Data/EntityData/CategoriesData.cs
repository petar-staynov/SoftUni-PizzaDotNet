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
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1333ipar.png",
                },
                new Category()
                {
                    Name = "Pizza",
                    Description = "Delicious pizzas made with high quality organic products and care.",
                    Products = ProductsData.GetPizzas(),
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1290medium.png",
                },
                new Category()
                {
                    Name = "Dips",
                    Description = "Carefully selected sauces for your favourite pizzas.",
                    Products = ProductsData.GetDips(),
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1206ipar.png",
                },
                new Category()
                {
                    Name = "Drinks",
                    Description = "none",
                    Products = ProductsData.GetDrinks(),
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/32501_bigcoke.jpg",
                },
                new Category()
                {
                    Name = "Desserts",
                    Description = "none",
                    Products = ProductsData.GetDesserts(),
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1549ipar.png",
                },
            };
        }
    }
}
