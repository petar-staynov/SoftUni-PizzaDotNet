namespace PizzaDotNet.Web.ViewModels.Orders
{
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class OrderAddressViewModel : IMapFrom<OrderAddress>
    {
        public string PersonName { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public string PhoneNumber { get; set; }
    }
}