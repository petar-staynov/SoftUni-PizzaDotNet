namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;

    public interface IOrdersService
    {
        int GetCount();

        decimal? GetTotalProfit();

        Task<Order> CreateAsync(Order order);

        Task<Order> UpdateAsync(Order order);

        T GetById<T>(int id);

        Order GetBaseById(int id);

        // TODO Make GetByUserIdSorted the default and only method
        IEnumerable<T> GetByUserId<T>(string userId);

        IEnumerable<T> GetByUserIdSorted<T>(string userId, string criteria);

        Order GetBaseByUserId(string userId);

        IEnumerable<T> GetAll<T>(string sortCriteria = null, int? count = null);


        Task<Order> ChangeStatus(int orderId,OrderStatusEnum statusEnum);
    }
}
