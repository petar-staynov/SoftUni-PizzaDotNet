namespace PizzaDotNet.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Common.Models;

    public class UserAddress : BaseDeletableModel<int>
    {
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public string PhoneNumber { get; set; }
    }
}
