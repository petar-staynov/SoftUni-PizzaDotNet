namespace PizzaDotNet.Services
{
    using AutoMapper;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Web.ViewModels.Products;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<ProductCreateSizeInputModel, SizeOfProduct>();
        }
    }
}
