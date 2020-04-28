namespace PizzaDotNet.Services
{
    using AutoMapper;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Web.ViewModels.Addresses;
    using PizzaDotNet.Web.ViewModels.Administration.Products;
    using PizzaDotNet.Web.ViewModels.Cart;
    using PizzaDotNet.Web.ViewModels.DTO;
    using PizzaDotNet.Web.ViewModels.Orders;
    using PizzaDotNet.Web.ViewModels.Products;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            /* ProductSize <--> AdminProductCreateSizeInputModel*/
            this.CreateMap<ProductSize, AdminProductCreateSizeInputModel>();
            this.CreateMap<AdminProductCreateSizeInputModel, ProductSize>();

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

            /* OrderAddress <--> CartAddressViewInputModel*/
            this.CreateMap<CartAddressViewInputModel, OrderAddress>();
            this.CreateMap<OrderAddress, CartAddressViewInputModel>();

            /* UserAddress <--> CartAddressViewInputModel*/
            this.CreateMap<UserAddress, CartAddressViewInputModel>();
            this.CreateMap<CartAddressViewInputModel, UserAddress>();

            /* OrderProduct <-> CartProductViewModel */
            this.CreateMap<OrderProduct, CartProductViewModel>();
            this.CreateMap<CartProductViewModel, OrderProduct>();

            /* OrderProduct <-> SessionCartProductDto */
            this.CreateMap<OrderProduct, SessionCartProductDto>();
            this.CreateMap<SessionCartProductDto, OrderProduct>();

            /* Product <-> OrderProduct */
            this.CreateMap<Product, OrderProduct>();
            this.CreateMap<OrderProduct, Product>();

            /* Order <-> OrderViewModel */
            this.CreateMap<Order, OrderViewModel>();
            this.CreateMap<OrderViewModel, Order>();

            /* Order <-> OrderDto */
            this.CreateMap<Order, OrderDto>();
            this.CreateMap<OrderDto, Order>();

            /* OrderStatus <-> OrderStatusViewModel */
            // this.CreateMap<OrderStatus, OrderStatusViewModel>();
            // this.CreateMap<OrderStatusViewModel, OrderStatus>();

            /* OrderProduct <-> OrderProductViewModel */
            // this.CreateMap<OrderProduct, OrderProductViewModel>();

            /* OrderAddress <-> OrderAddressViewModel */
            // this.CreateMap<OrderAddress, OrderAddressViewModel>();
            // this.CreateMap<OrderAddressViewModel, OrderAddress>();
        }
    }
}
