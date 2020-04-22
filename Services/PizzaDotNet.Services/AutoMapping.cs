namespace PizzaDotNet.Services
{
    using AutoMapper;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Models.DTO;
    using PizzaDotNet.Web.ViewModels.Products;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<ProductCreateSizeInputModel, SizeOfProduct>();
            this.CreateMap<ProductViewInputModel, SessionCartProductDto>();
        }
    }
}
