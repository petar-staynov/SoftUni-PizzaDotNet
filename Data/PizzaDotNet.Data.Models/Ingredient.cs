namespace PizzaDotNet.Data.Models
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Common.Models;

    public class Ingredient : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<ProductsIngredients> Products { get; set; }
    }
}
