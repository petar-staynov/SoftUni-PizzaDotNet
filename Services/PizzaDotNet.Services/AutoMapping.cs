namespace PizzaDotNet.Services
{
    using AutoMapper;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Products;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<ProductCreateSizeInputModel, ProductSize>();

            this.CreateMap<ProductViewInputModel, SessionCartProductDto>();

            this.CreateMap<SessionCartProductDto, CartProductViewModel>();
            this.CreateMap<CartProductViewModel, SessionCartProductDto>();

            this.CreateMap<SessionCartDto, CartViewModel>();
        }
    }
}
