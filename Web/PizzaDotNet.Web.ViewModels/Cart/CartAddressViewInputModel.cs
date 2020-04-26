namespace PizzaDotNet.Web.ViewModels.Cart
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CartAddressViewInputModel : IMapFrom<UserAddress>
    {
        [Required]
        [Display(Name = "Personal name")]
        public string PersonName { get; set; }

        [Required]
        public string Area { get; set; }

        [Required]
        public string Street { get; set; }

        public string Building { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
