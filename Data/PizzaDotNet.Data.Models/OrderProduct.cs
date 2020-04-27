namespace PizzaDotNet.Data.Models
{
    using PizzaDotNet.Data.Common.Models;
    using PizzaDotNet.Services.Mapping;

    public class OrderProduct
    {
        public int OderId { get; set; }

        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        /*
         * Save a copy of Product Name, Size and Price since they can change over time
         */

        public string Name { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice => this.Quantity * this.Price;
    }
}
