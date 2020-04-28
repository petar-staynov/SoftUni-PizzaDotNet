namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminProductSizeViewModel : IMapFrom<ProductSize>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
