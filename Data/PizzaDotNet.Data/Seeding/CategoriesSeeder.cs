// namespace PizzaDotNet.Data.Seeding
// {
//     using System;
//     using System.Collections.Generic;
//     using System.Linq;
//     using System.Threading.Tasks;
//
//     using PizzaDotNet.Data.Models;
//
//     public class CategoriesSeeder : ISeeder
//     {
//         public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
//         {
//             if (!dbContext.Categories.Any())
//             {
//                 var categories = new List<string>
//                 {
//                     "Starters",
//                     "Pizzas",
//                     "Dips",
//                     "Drinks",
//                     "Desserts",
//                 };
//
//                 foreach (string categoryString in categories)
//                 {
//                     await dbContext.Categories.AddAsync(new Category
//                     {
//                         Name = categoryString,
//                         Description = categoryString,
//                     });
//                 }
//             }
//         }
//     }
// }
