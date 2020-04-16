namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductCreateSizeInputModel : IMapFrom<SizeOfProduct>
    {
        public ProductCreateSizeInputModel()
        {
            this.Size = "Default";
        }

        [Required]
        [MinLength(1)]
        public string Size { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        [Range(0, 999.99)]
        public decimal Price { get; set; }
    }
}
