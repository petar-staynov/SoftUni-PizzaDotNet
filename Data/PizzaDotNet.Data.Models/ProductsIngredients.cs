namespace PizzaDotNet.Data.Models
{
    using PizzaDotNet.Data.Common.Models;

    public class ProductsIngredients : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
