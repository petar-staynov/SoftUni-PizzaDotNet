namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductCreateSizeInputModel
    {
        public ProductCreateSizeInputModel()
        {
            this.Price = -1;
        }

        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        [Range(-1, 999.99)]
        public decimal Price { get; set; }
    }
}
