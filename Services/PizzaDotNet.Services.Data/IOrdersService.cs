namespace PizzaDotNet.Services.Data
{
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface IOrdersService
    {
        Task<Order> CreateAsync(Order order);

        Task<Order> UpdateAsync(Order order);

        T GetById<T>(int id);

        Order GetBaseById(int id);

        T GetByUserId<T>(string userId);

        Order GetBaseByUserId(string userId);
    }
}
