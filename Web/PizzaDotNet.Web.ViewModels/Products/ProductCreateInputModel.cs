namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Categories;

    public class ProductCreateInputModel : IMapFrom<Product>
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        // [Required] // TODO Enable this
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        // [MaxFileSize(1 * 1024 * 1024)] // TODO Enable these
        // [PermittedExtensions(new string[] { ".jpg", ".png", ".gif",".bmp", "jpeg"})]
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public string ImageStorageName { get; set; }
    }
}
