namespace PizzaDotNet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class OrdersService : IOrdersService
    {
        private readonly IRepository<Order> orderRepository;

        public OrdersService(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public Task<Order> UpdateAsync(Order order)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            var order = this.orderRepository
                .All()
                .Where(o => o.Id == id)
                .To<T>()
                .FirstOrDefault();

            return order;
        }

        public Order GetBaseById(int id)
        {
            var order = this.orderRepository
                .All()
                .FirstOrDefault(o => o.Id == id);

            return order;
        }

        public T GetByUserId<T>(string userId)
        {
            var order = this.orderRepository
                .All()
                .Where(o => o.UserId == userId)
                .To<T>()
                .FirstOrDefault();

            return order;
        }

        public Order GetBaseByUserId(string userId)
        {
            var order = this.orderRepository
                .All()
                .FirstOrDefault(o => o.UserId == userId);

            return order;
        }
    }
}
