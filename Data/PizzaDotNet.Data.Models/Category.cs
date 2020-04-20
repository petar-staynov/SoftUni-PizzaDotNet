namespace PizzaDotNet.Data.Models
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string ImageUrl { get; set; }

        public string ImageStorageName { get; set; }
    }
}
