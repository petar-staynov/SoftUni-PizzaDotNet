namespace PizzaDotNet.Services.Data.Tests.Models
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CategoryServiceModel : IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
