namespace PizzaDotNet.Data.Models
{
    public class OrderAddress
    {
        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int UserAddressId { get; set; }

        public virtual UserAddress UserAddress { get; set; }


        /*
         * Create a copy of the address since the user can change his over time
         */

        public string PersonName { get; set; }

        public string Area { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public string PhoneNumber { get; set; }
    }
}
