namespace PizzaDotNet.Web.ViewModels.Orders
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminOrderProductViewModel : IMapFrom<OrderProduct>
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
