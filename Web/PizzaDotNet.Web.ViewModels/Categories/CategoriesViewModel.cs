namespace PizzaDotNet.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public IEnumerable<CategoriesCategoryViewModel> Categories { get; set; }
    }
}
