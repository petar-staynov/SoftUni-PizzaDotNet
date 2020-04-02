namespace PizzaDotNet.Web.ViewModels.Categories
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CategoryProductViewModel : IMapFrom<Product>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
