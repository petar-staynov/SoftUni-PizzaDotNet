namespace PizzaDotNet.Data.Models
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Common.Models;

    public class OrderStatus : BaseModel<int>
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
