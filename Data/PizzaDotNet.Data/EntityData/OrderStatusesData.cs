namespace PizzaDotNet.Data.EntityData
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;

    public static class OrderStatusesData
    {
        public static IEnumerable<OrderStatus> GetStatuses()
        {
            return new List<OrderStatus>()
            {
                new OrderStatus
                {
                    Status = "Processing",
                },
                new OrderStatus
                {
                    Status = "Shipped",
                },
                new OrderStatus
                {
                    Status = "Delivered",
                },
                new OrderStatus
                {
                    Status = "Cancelled",
                },
                new OrderStatus
                {
                    Status = "Refunded",
                },
            };
        }
    }
}
