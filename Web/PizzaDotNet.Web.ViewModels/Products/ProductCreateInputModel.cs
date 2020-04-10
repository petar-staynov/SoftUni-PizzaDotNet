namespace PizzaDotNet.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Common.CustomValidationAttributes;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Categories;

    public class ProductCreateInputModel : IMapFrom<Product>
    {
        [Required] public string Name { get; set; }

        public string Description { get; set; }

        [Required] public decimal Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        [DataType(DataType.ImageUrl)] 
        public string ImageUrl { get; set; }

        [Display(Name = "Image")]
        [MaxFileSize(1, "mb")]
        [AllowedExtensions(new string[] {".jpg"})]
        public IFormFile ImageFile { get; set; }

        public string ImageStorageName { get; set; }
    }
}
