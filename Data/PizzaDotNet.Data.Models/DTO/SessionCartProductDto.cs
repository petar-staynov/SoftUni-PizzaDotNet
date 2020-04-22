namespace PizzaDotNet.Data.Models.DTO
{
    public class SessionCartProductDto
    {
        private decimal totalPrice;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice
        {
            get => this.Quantity * this.Price;
            set => this.totalPrice = value;
        }

        public string ImageUrl { get; set; }
    }
}
