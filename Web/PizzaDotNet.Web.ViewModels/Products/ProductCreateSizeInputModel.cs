namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductCreateSizeInputModel : IMapFrom<SizeOfProduct>
    {
        [DefaultValue("Default")]
        public string Size { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }
    }
}
