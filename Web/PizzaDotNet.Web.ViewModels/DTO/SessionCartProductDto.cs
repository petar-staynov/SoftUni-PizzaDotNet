namespace PizzaDotNet.Web.ViewModels.DTO
{
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Cart;

    public class SessionCartProductDto
    {
        public int Id { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }
    }
}
