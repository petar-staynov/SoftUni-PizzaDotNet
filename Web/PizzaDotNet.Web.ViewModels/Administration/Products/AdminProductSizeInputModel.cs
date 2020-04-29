namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminProductSizeInputModel : IMapFrom<ProductSize>
    {
        // [Required]
        // public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }
    }
}
