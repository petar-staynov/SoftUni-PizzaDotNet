namespace PizzaDotNet.Data.Models
{
    using PizzaDotNet.Data.Common.Models;

    public class OrderStatus : BaseModel<int>
    {
        public string Status { get; set; }
    }
}
