using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> orderStatusRepository;

        public OrderStatusService(IRepository<OrderStatus> orderStatusRepository)
        {
            this.orderStatusRepository = orderStatusRepository;
        }

        public async Task<OrderStatus> GetById(int id)
        {
            var orderStatus = await this.orderStatusRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return orderStatus;
        }

        public async Task<OrderStatus> GetByName(string name)
        {
            var orderStatus = await this.orderStatusRepository
                .All()
                .FirstOrDefaultAsync(x => x.Status == name);

            return orderStatus;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var query = this.orderStatusRepository
                .All();

            if (typeof(T) == typeof(OrderStatus))
            {
                var statuses = query.ToList<OrderStatus>();
                return statuses as IEnumerable<T>;
            }

            var result = await query.To<T>().ToListAsync();

            return result;
        }
    }
}
