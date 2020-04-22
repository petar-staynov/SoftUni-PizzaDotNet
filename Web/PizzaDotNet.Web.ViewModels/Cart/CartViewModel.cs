namespace PizzaDotNet.Web.ViewModels.Cart
{
    using System.Collections.Generic;

    using PizzaDotNet.Data.Models.DTO;

    public class CartViewModel
    {
        public CartViewModel()
        {
            this.Products = new HashSet<SessionCartProductDto>();
        }

        public ICollection<SessionCartProductDto> Products { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
