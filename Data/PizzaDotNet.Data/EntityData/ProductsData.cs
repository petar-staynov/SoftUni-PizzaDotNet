namespace PizzaDotNet.Data.EntityData
{
    using System;
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;

    public static class ProductsData
    {
        private static readonly Random Rng = new Random();

        public static ICollection<Product> GetPizzas()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "Basic Cheese",
                    Description = "Cheesus, that's a lot of cheese.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1265medium.png",
                },
                new Product
                {
                    Name = "Kaprichoza",
                    Description = "A classic!",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1290medium.png",
                },
                new Product
                {
                    Name = "Garden Peperoni",
                    Description = "Classic peperoni but with vegetables.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1291medium.png",
                },
                new Product
                {
                    Name = "Garden Ham",
                    Description = "Vegetables and ham, what more could you want?",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1292medium.png",
                },
                new Product
                {
                    Name = "Pure Peperoni",
                    Description = "The timeless classic.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1293medium.png",
                },
                new Product
                {
                    Name = "Garden",
                    Description = "No animals were harmed in the making of this pizza.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1295medium.png",
                },
                new Product
                {
                    Name = "Shredder",
                    Description = "Shred and tear.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1298medium.png",
                },
                new Product
                {
                    Name = "Mars",
                    Description = "Contains a lot of orange, except oranges.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1300medium.png",
                },
                new Product
                {
                    Name = "Venus",
                    Description = "All kinds of cheese.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1302medium.png",
                },
                new Product
                {
                    Name = "The Rock",
                    Description = "It looks like a rock.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1312medium.png",
                },
                new Product
                {
                    Name = "Exotica",
                    Description = "Pineapple pizza for the more extravagant.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1313medium.png",
                },
                new Product
                {
                    Name = "Kelvin",
                    Description = "Very hot.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1314medium.png",
                },
                new Product
                {
                    Name = "Modern Italian",
                    Description = "Just like the italians but bigger and denser.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1341medium.png",
                },
                new Product
                {
                    Name = "Yellowstone",
                    Description = "It's very yellow.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1342medium.png",
                },
                new Product
                {
                    Name = "Big Boss",
                    Description = "This one has everything.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1344medium.png",
                },
                new Product
                {
                    Name = "Forest",
                    Description = "The green stuff is probably healthy.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1355medium.png",
                },
                new Product
                {
                    Name = "Tomato Hell",
                    Description = "Mama Mia, that's a lot of tomatoes.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1361medium.png",
                },
                new Product
                {
                    Name = "Tomato Vegan",
                    Description = "More tomatoes, less meat.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1363medium.png",
                },
                new Product
                {
                    Name = "Meatmania",
                    Description = "Lots of animal protein..",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1364medium.png",
                },
                new Product
                {
                    Name = "Burger Pizza",
                    Description = "It's a burger but turned into a pizza.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1505medium.png",
                },
                new Product
                {
                    Name = "Sauce please",
                    Description = "Bring on the sauce!",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1535medium.png",
                },
                new Product
                {
                    Name = "Star",
                    Description = "Peperoni in the shape of a star. Who even makes these things?",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1593medium.png",
                },
                new Product
                {
                    Name = "Random",
                    Description = "We'll just throw whatever's left",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1609medium.png",
                },
                new Product
                {
                    Name = "Devil",
                    Description = "Red and spicy",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1616medium.png",
                },
                new Product
                {
                    Name = "Creamy",
                    Description = "Cream cheese for all.",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1617medium.png",
                },
                new Product
                {
                    Name = "Striped",
                    Description = "The white stuff is just ranch sauce, we swear!",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1616medium.png",
                },
            };

            // Generate sizes
            foreach (var product in products)
            {
                product.Sizes = new List<ProductSize>()
                {
                    new ProductSize()
                    {
                        Name = "Small",
                        Price = Rng.Next(5, 10),
                    },
                    new ProductSize()
                    {
                        Name = "Medium",
                        Price = Rng.Next(10, 15),
                    },
                    new ProductSize()
                    {
                        Name = "Large",
                        Price = Rng.Next(15, 20),
                    },
                };
            }

            return products;
        }

        public static ICollection<Product> GetStarters()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Chese sticks",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1230ipar.png",
                },
                new Product
                {
                    Name = "Brazilian Bread",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1333ipar.png",
                },
            };

            foreach (var product in products)
            {
                product.Sizes = new List<ProductSize>()
                {
                    new ProductSize()
                    {
                        Name = "Default",
                        Price = Rng.Next(4, 8),
                    },
                };
            }

            return products;
        }

        public static ICollection<Product> GetDips()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "BBQ",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1200ipar.png",
                },
                new Product
                {
                    Name = "Garlic",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1201ipar.png",
                },
                new Product
                {
                    Name = "Cream",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1205ipar.png",
                },
                new Product
                {
                    Name = "Tomato",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1206ipar.png",
                },
            };

            foreach (var product in products)
            {
                product.Sizes = new List<ProductSize>()
                {
                    new ProductSize()
                    {
                        Name = "Default",
                        Price = Rng.Next(3, 4),
                    },
                };
            }

            return products;
        }

        public static ICollection<Product> GetDrinks()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Orange juice",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/2018-328605.jpg",
                },
                new Product
                {
                    Name = "Coke",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/32501_bigcoke.jpg",
                },
                new Product
                {
                    Name = "Fanta",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/fanta-orange-150ml.jpg",
                },
                new Product
                {
                    Name = "Water",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/fiji-bottle-500ml-front_900x900.png",
                },
                new Product
                {
                    Name = "Beer",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/s-l300.jpg",
                },
            };

            foreach (var product in products)
            {
                product.Sizes = new List<ProductSize>()
                {
                    new ProductSize()
                    {
                        Name = "Default",
                        Price = Rng.Next(2, 4),
                    },
                };
            }

            return products;
        }

        public static ICollection<Product> GetDesserts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Muffin",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1228ipar.png",
                },
                new Product
                {
                    Name = "Icecream Vanilla",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1335ipar.png",
                },
                new Product
                {
                    Name = "Choco Pie",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1516ipar.png",
                },
                new Product
                {
                    Name = "Icecream Chocolate",
                    Description = "none",
                    ImageUrl = "https://storage.googleapis.com/pizzadotnet_bucket/1549ipar.png",
                },
            };

            foreach (var product in products)
            {
                product.Sizes = new List<ProductSize>()
                {
                    new ProductSize()
                    {
                        Name = "Default",
                        Price = Rng.Next(2, 5),
                    },
                };
            }

            return products;
        }
    }
}
