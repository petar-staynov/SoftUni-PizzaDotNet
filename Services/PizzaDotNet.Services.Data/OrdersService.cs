namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;
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

        public async Task<int> GetCount()
        {
            int count = this.orderRepository
                .All()
                .Count();

            return count;
        }

        public async Task<decimal?> GetTotalProfit()
        {
            decimal? profit = this.orderRepository
                .All()
                .Sum(o => o.TotalPriceDiscounted);

            return profit;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();

            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();
        }

        public void Update(Order order)
        {
            this.orderRepository.Update(order);
            this.orderRepository.SaveChanges();
        }

        public async Task<T> GetById<T>(int id)
        {
            var order = await this.orderRepository
                .All()
                .Where(o => o.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<Order> GetBaseById(int id)
        {
            var order = this.orderRepository
                .All()
                .Where(o => o.Id == id)
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .Include(o => o.OrderAddress)
                .Include(o => o.OrderStatus)
                .FirstOrDefault();

            return order;
        }

        public async Task<IEnumerable<T>> GetByUserId<T>(string userId, string sortCriteria = null)
        {
            var ordersQuery = this.orderRepository
                .All()
                .Where(o => o.UserId == userId);


            switch (sortCriteria)
            {
                case SortingCriterias.ORDER_PRICE_HIGHEST_TO_LOWEST:
                    ordersQuery = ordersQuery.OrderBy(o => o.TotalPrice);
                    break;
                case SortingCriterias.ORDER_PRICE_LOWEST_TO_HIGHEST:
                    ordersQuery = ordersQuery.OrderByDescending(o => o.TotalPrice);
                    break;
                case SortingCriterias.ORDER_DATE_OLDEST_TO_NEWEST:
                    ordersQuery = ordersQuery.OrderBy(o => o.CreatedOn);
                    break;
                case SortingCriterias.ORDER_DATE_NEWEST_TO_OLDEST:
                    ordersQuery = ordersQuery.OrderByDescending(o => o.CreatedOn);
                    break;
            }

            var orders = await ordersQuery.To<T>().ToListAsync();

            return orders;
        }

        public async Task<Order> GetBaseByUserId(string userId)
        {
            var order = await this.orderRepository
                .All()
                .FirstOrDefaultAsync(o => o.UserId == userId);

            return order;
        }

        public async Task<IEnumerable<T>> GetAll<T>(string sortCriteria = null, int? count = null)
        {
            var ordersQuery = this.orderRepository
                .All();

            switch (sortCriteria)
            {
                case SortingCriterias.ORDER_PRICE_HIGHEST_TO_LOWEST:
                    ordersQuery = ordersQuery.OrderBy(o => o.TotalPrice);
                    break;
                case SortingCriterias.ORDER_PRICE_LOWEST_TO_HIGHEST:
                    ordersQuery = ordersQuery.OrderByDescending(o => o.TotalPrice);
                    break;
                case SortingCriterias.ORDER_DATE_OLDEST_TO_NEWEST:
                    ordersQuery = ordersQuery.OrderBy(o => o.CreatedOn);
                    break;
                case SortingCriterias.ORDER_DATE_NEWEST_TO_OLDEST:
                    ordersQuery = ordersQuery.OrderByDescending(o => o.CreatedOn);
                    break;
                case SortingCriterias.ORDER_USENAME_DESCENDING:
                    ordersQuery = ordersQuery.OrderByDescending(o => o.User.UserName);
                    break;
                case SortingCriterias.ORDER_USERNAME_ASCENDING:
                    ordersQuery = ordersQuery.OrderBy(o => o.User.UserName);
                    break;
            }

            if (typeof(T) == typeof(Order))
            {
                var orders = ordersQuery.ToList<Order>();
                return orders as IEnumerable<T>;
            }

            var result = await ordersQuery.To<T>().ToListAsync();

            return result;
        }

        public async Task<Order> ChangeStatus(int orderId, OrderStatusEnum statusEnum)
        {
            var order = await this.orderRepository
                .All()
                .Include(o => o.OrderStatus)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            order.OrderStatus = await this.orderStatusService.GetByName(statusEnum.ToString());

            this.orderRepository.Update(order);
            await this.orderRepository.SaveChangesAsync();

            return null;
        }
    }
}
