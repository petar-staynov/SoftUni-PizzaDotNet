using Microsoft.EntityFrameworkCore;
using PizzaDotNet.Data.Models.Enums;

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
        private readonly IOrderStatusService orderStatusService;

        public OrdersService(IRepository<Order> orderRepository, IOrderStatusService orderStatusService)
        {
            this.orderRepository = orderRepository;
            this.orderStatusService = orderStatusService;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public T GetById<T>(int id)
        {
            var order = this.orderRepository
                .All()
                .Where(o => o.Id == id)
                .To<T>()
                .FirstOrDefault();

            // TODO REMOVE THESE
            var baseEnt = this.orderRepository
                .All()
                .FirstOrDefault(o => o.Id == id);

            var fullEnt = this.orderRepository
                .All()
                .Include(o => o.OrderStatus)
                .Include(o => o.OrderProducts)
                .Include(o => o.OrderAddress)
                .FirstOrDefault(o => o.Id == id);

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

        public async Task<Order> ChangeStatus(int orderId, OrderStatusEnum statusEnum)
        {
            var order = this.orderRepository
                .All()
                .Include(o => o.OrderStatus)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            order.OrderStatus = this.orderStatusService.GetByName(statusEnum.ToString());

            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();

            return null;
        }
    }
}
