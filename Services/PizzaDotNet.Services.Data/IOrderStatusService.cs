namespace PizzaDotNet.Services.Data
{
    using PizzaDotNet.Data.Models;

    public interface IOrderStatusService
    {
        OrderStatus GetById(int id);

        OrderStatus GetByName(string name);
    }
}
