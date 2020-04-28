namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.Enums;

    public interface IOrderStatusService
    {
        OrderStatus GetById(int id);

        OrderStatus GetByName(string name);

        IEnumerable<T> GetAll<T>();
    }
}
