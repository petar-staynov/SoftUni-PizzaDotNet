namespace PizzaDotNet.Web.ViewModels.Categories
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        // public IEnumerable<Product> Products { get; set; }
    }
}
