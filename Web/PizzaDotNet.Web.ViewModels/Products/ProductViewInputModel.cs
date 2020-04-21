namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.SizeOfProduct;

    public class ProductViewInputModel : IMapFrom<Product>
    {
        private ICollection<SizeOfProductViewModel> sizes;

        public ProductViewInputModel()
        {
            this.Quantity = 1;
            this.Size = "Unspecified";
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a valid size")]
        public string Size { get; set; }

        public ICollection<SizeOfProductViewModel> Sizes
        {
            get => this.sizes.OrderBy(x => x.Price).ToList();
            set => this.sizes = value;
        }

        [Display(Name="Quantity")]
        [Range(1, 20)]
        [Required(ErrorMessage = "Please enter a a valid quantity")]
        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        public double? Rating { get; set; }
    }
}
