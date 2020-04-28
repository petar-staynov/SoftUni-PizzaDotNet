namespace PizzaDotNet.Web.ViewModels.Administration.Orders
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminOrderStatusViewModel : IMapFrom<OrderStatus>
    {
        public int Id { get; set; }

        public string Status { get; set; }
    }
}
