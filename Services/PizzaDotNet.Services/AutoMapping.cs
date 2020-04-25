namespace PizzaDotNet.Services
{
    using AutoMapper;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Web.ViewModels.Addresses;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Products;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            /* ProductSize <--> ProductCreateSizeInputModel*/
            this.CreateMap<ProductSize, ProductCreateSizeInputModel>();
            this.CreateMap<ProductCreateSizeInputModel, ProductSize>();

            /* ProductViewInputModel <--> SessionCartProductDto*/
            this.CreateMap<ProductViewInputModel, SessionCartProductDto>();
            this.CreateMap<SessionCartProductDto, ProductViewInputModel>();

            /* CartProductViewModel <--> SessionCartProductDto*/
            this.CreateMap<SessionCartProductDto, CartProductViewModel>();
            this.CreateMap<CartProductViewModel, SessionCartProductDto>();

            /* CartViewModel <--> SessionCartDto*/
            this.CreateMap<SessionCartDto, CartViewModel>();
            this.CreateMap<CartViewModel, SessionCartDto>();

            /* UserAddress <--> AddressViewInputModel*/
            this.CreateMap<AddressViewInputModel, UserAddress>();
            this.CreateMap<UserAddress, AddressViewInputModel>();
        }
    }
}
