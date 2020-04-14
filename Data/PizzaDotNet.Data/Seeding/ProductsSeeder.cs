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
                    // PIZZAS
                    new Product { Name = "Basic Cheese", Description = "Cheesus, that's a lot of cheese.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1265medium.png", CategoryId = 2 },
                    new Product { Name = "Kaprichoza", Description = "A classic!", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1266medium.png", CategoryId = 2 },
                    new Product { Name = "Garden Peperoni", Description = "Classic peperoni but with vegetables.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1291medium.png", CategoryId = 2 },
                    new Product { Name = "Garden Ham", Description = "Vegetables and ham, what more could you want?", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1292medium.png", CategoryId = 2 },
                    new Product { Name = "Pure Peperoni", Description = "The timeless classic.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1293medium.png", CategoryId = 2 },
                    new Product { Name = "Garden", Description = "No animals were harmed in the making of this pizza.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1295medium.png", CategoryId = 2 },
                    new Product { Name = "Shredder", Description = "Shred and tear.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1298medium.png", CategoryId = 2 },
                    new Product { Name = "Mars", Description = "Contains a lot of orange, except oranges.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1300medium.png", CategoryId = 2 },
                    new Product { Name = "Venus", Description = "All kinds of cheese.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1302medium.png", CategoryId = 2 },
                    new Product { Name = "The Rock", Description = "It looks like a rock.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1312medium.png", CategoryId = 2 },
                    new Product { Name = "Exotica", Description = "Pineapple pizza for the more extravagant.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1313medium.png", CategoryId = 2 },
                    new Product { Name = "Kelvin", Description = "Very hot.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1314medium.png", CategoryId = 2 },
                    new Product { Name = "Modern Italian", Description = "Just like the italians but bigger and denser.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1341medium.png", CategoryId = 2 },
                    new Product { Name = "Yellowstone", Description = "It's very yellow.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1342medium.png", CategoryId = 2 },
                    new Product { Name = "Big Boss", Description = "This one has everything.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1344medium.png", CategoryId = 2 },
                    new Product { Name = "Forest", Description = "The green stuff is probably healthy.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1355medium.png", CategoryId = 2 },
                    new Product { Name = "Tomato Hell", Description = "Mama Mia, that's a lot of tomatoes.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1361medium.png", CategoryId = 2 },
                    new Product { Name = "Tomato Vegan", Description = "More tomatoes, less meat.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1363medium.png", CategoryId = 2 },
                    new Product { Name = "Meatmania", Description = "Lots of animal protein..", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1364medium.png", CategoryId = 2 },
                    new Product { Name = "Burger Pizza", Description = "It's a burger but turned into a pizza.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1505medium.png", CategoryId = 2 },
                    new Product { Name = "Sauce please", Description = "Bring on the sauce!", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1535medium.png", CategoryId = 2 },
                    new Product { Name = "Star", Description = "Peperoni in the shape of a star. Who even makes these things?", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1593medium.png", CategoryId = 2 },
                    new Product { Name = "Random", Description = "We'll just throw whatever's left", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1609medium.png", CategoryId = 2 },
                    new Product { Name = "Devil", Description = "Red and spicy", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1616medium.png", CategoryId = 2 },
                    new Product { Name = "Creamy", Description = "Cream cheese for all.", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1617medium.png", CategoryId = 2 },
                    new Product { Name = "Striped", Description = "The white stuff is just ranch sauce, we swear!", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1616medium.png", CategoryId = 2 },

                    // STARTERS
                    new Product { Name = "Chese sticks", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1230ipar.png", CategoryId = 2 },
                    new Product { Name = "Brazilian Bread", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1333ipar.png", CategoryId = 2 },

                    // SAUCES
                    new Product { Name = "BBQ", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1200ipar.png", CategoryId = 2 },
                    new Product { Name = "Garlic", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1201ipar.png", CategoryId = 2 },
                    new Product { Name = "Cream", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1205ipar.png", CategoryId = 2 },
                    new Product { Name = "Tomato", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1206ipar.png", CategoryId = 2 },
                    new Product { Name = "Tomato", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1206ipar.png", CategoryId = 2 },

                    // DRINKS
                    new Product { Name = "Orange juice", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/2018-328605.jpg", CategoryId = 2 },
                    new Product { Name = "Coke", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/32501_bigcoke.jpg", CategoryId = 2 },
                    new Product { Name = "Fanta", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/fanta-orange-150ml.jpg", CategoryId = 2 },
                    new Product { Name = "Water", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/fiji-bottle-500ml-front_900x900.png", CategoryId = 2 },
                    new Product { Name = "Beer", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/s-l300.jpg", CategoryId = 2 },

                    // DESSERTS
                    new Product { Name = "Muffin", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1228ipar.png", CategoryId = 2 },
                    new Product { Name = "Icecream Vanilla", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1335ipar.png", CategoryId = 2 },
                    new Product { Name = "Choco Pie", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1516ipar.png", CategoryId = 2 },
                    new Product { Name = "Icecream Chocolate", Description = "", ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1549ipar.png", CategoryId = 2 },

                };

                foreach (Product product in products)
                {
                    await dbContext.Products.AddRangeAsync(products);
                }
            }
        }
    }
}
