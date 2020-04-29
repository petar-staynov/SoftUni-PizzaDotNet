namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;

    public interface IOrdersService
    {
        Task<int> GetCount();

        Task<decimal?> GetTotalProfit();

        Task<Order> CreateAsync(Order order);

        Task UpdateAsync(Order order);

        Task<T> GetById<T>(int id);

        Task<Order> GetBaseById(int id);

        Task<IEnumerable<T>> GetByUserId<T>(string userId, string sortCriteria = null);

        Task<IEnumerable<T>> GetAll<T>(string sortCriteria = null, int? count = null);

        Task<Order> ChangeStatus(int orderId, OrderStatusEnum statusEnum);
    }
}
