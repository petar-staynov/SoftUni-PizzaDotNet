namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminProductCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
