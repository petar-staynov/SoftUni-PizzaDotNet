namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AdminProductCategoryInputModel : IMapFrom<Category>
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
