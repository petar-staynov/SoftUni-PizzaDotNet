namespace PizzaDotNet.Web.ViewModels.ProductSize
{
    using PizzaDotNet.Services.Mapping;

    public class ProductSizeViewModel : IMapFrom<Data.Models.ProductSize>
    {
        public string Size { get; set; }

        public decimal Price { get; set; }
    }
}
