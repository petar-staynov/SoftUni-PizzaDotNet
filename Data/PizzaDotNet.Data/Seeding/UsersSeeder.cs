namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using PizzaDotNet.Data.EntityData;
    using PizzaDotNet.Data.Models;

    internal class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var admin = new ApplicationUser
            {
                Email = "admin@mail.com",
                UserName = "admin@mail.com",
                EmailConfirmed = true,
            };
            await SeedAdminAsync(userManager, admin, "admin1234");

            var mod = new ApplicationUser
            {
                Email = "mod@mail.com",
                UserName = "mod@mail.com",
                EmailConfirmed = true,
            };
            await SeedModeratorAsync(userManager, mod, "mod1234");

            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
                EmailConfirmed = true,
            };
            await SeedUserAsync(userManager, user, "user1234");
        }

        private static async Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string password)
        {
            var userEntity = await userManager.FindByEmailAsync(user.Email);
            if (userEntity == null)
            {
                var userCreateResult = await userManager.CreateAsync(user, password);
                var roleAssignResult = await userManager.AddToRoleAsync(user, "Administrator");

                if (!userCreateResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, userCreateResult.Errors.Select(e => e.Description)));
                }

                if (!roleAssignResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, roleAssignResult.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedModeratorAsync(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string password)
        {
            var userEntity = await userManager.FindByEmailAsync(user.Email);
            if (userEntity == null)
            {
                var userCreateResult = await userManager.CreateAsync(user, password);
                var roleAssignResult = await userManager.AddToRoleAsync(user, "Manager");

                if (!userCreateResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, userCreateResult.Errors.Select(e => e.Description)));
                }

                if (!roleAssignResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, roleAssignResult.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedUserAsync(
            UserManager<ApplicationUser> userManager,
            ApplicationUser user,
            string password)
        {
            var userEntity = await userManager.FindByEmailAsync(user.Email);
            if (userEntity == null)
            {
                var userCreateResult = await userManager.CreateAsync(user, password);

                if (!userCreateResult.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, userCreateResult.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
