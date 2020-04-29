namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AdminProductCreateSizeInputModel
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        [Range(-1, 999.99)]
        public decimal? Price { get; set; }
    }
}
