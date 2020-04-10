namespace PizzaDotNet.Data.Models
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
