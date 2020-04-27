namespace PizzaDotNet.Web.ViewModels.ProductSize
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductSizeViewModel : IMapFrom<ProductSize>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
