namespace PizzaDotNet.Web.ViewModels.Administration.Categories
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminCategoriesCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ProductsCount { get; set; }

        public string ImageUrl { get; set; }

        public string ImageStorageName { get; set; }
    }
}
