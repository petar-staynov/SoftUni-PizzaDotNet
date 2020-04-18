namespace PizzaDotNet.Web.ViewModels.SizeOfProduct
{
    using PizzaDotNet.Services.Mapping;

    public class SizeOfProductViewModel : IMapFrom<Data.Models.SizeOfProduct>
    {
        public string Size { get; set; }

        public decimal Price { get; set; }
    }
}
