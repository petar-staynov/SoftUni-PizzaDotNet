namespace PizzaDotNet.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;
    using PizzaDotNet.Data.EntityData;

    public class OrderStatusesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrdersStatus.Any())
            {
                return;
            }

            var data = OrderStatusesData.GetStatuses();
            await dbContext.OrdersStatus.AddRangeAsync(data);
        }
    }
}
