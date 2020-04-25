namespace PizzaDotNet.Web.ViewModels.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AddressViewInputModel : IMapFrom<UserAddress>
    {
        public string PersonName { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
