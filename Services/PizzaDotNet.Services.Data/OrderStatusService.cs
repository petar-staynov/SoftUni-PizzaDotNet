namespace PizzaDotNet.Services.Data
{
    using System.Linq;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;

    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> orderStatusRepository;

        public OrderStatusService(IRepository<OrderStatus> orderStatusRepository)
        {
            this.orderStatusRepository = orderStatusRepository;
        }

        public OrderStatus GetById(int id)
        {
            var orderStatus = this.orderStatusRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            return orderStatus;
        }

        public OrderStatus GetByName(string name)
        {
            var orderStatus = this.orderStatusRepository
                .All()
                .FirstOrDefault(x => x.Status == name);

            return orderStatus;
        }
    }
}
