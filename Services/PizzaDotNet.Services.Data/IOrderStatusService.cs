namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;

    public interface IOrderStatusService
    {
        Task<OrderStatus> GetById(int id);

        Task<OrderStatus> GetByName(string name);

        Task<IEnumerable<T>> GetAll<T>();
    }
}
