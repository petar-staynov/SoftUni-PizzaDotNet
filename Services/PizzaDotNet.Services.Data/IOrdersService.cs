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

        Task UpdateAsync(Order order);

        void Update(Order order);

        Task<bool> DeleteAsync(int orderId);

        T GetById<T>(int id);

        Order GetBaseById(int id);

        IEnumerable<T> GetByUserId<T>(string userId, string sortCriteria = null);

        Order GetBaseByUserId(string userId);

        IEnumerable<T> GetAll<T>(string sortCriteria = null, int? count = null);


        Task<Order> ChangeStatus(int orderId, OrderStatusEnum statusEnum);
    }
}
