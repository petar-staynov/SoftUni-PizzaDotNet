namespace PizzaDotNet.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDe =>
            this.Description?.Length > 50
                ? this.Description?.Substring(0, 100) + "..."
                : this.Description;

        public IEnumerable<CategoryProductViewModel> Products { get; set; }
    }
}
