namespace PizzaDotNet.Web.ViewModels.Orders
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class OrderStatusViewModel : IMapFrom<OrderStatus>
    {
        public string Status { get; set; }
    }
}
