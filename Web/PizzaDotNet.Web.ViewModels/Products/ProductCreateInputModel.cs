namespace PizzaDotNet.Web.ViewModels.Products
{
    using System;
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
        private List<ProductCreateSizeInputModel> _sizes;

        public ProductCreateInputModel()
        {
            this.Sizes = new List<ProductCreateSizeInputModel>();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sizes")]
        [ProductSizesAttribute]
        public List<ProductCreateSizeInputModel> Sizes { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Display(Name = "Image")]
        [MaxFileSize(1, "mb")]
        [AllowedExtensions(new string[] { ".jpg", "jpeg", ".png", ".gif", ".bmp" })]
        public IFormFile ImageFile { get; set; }

        public string ImageStorageName { get; set; }
    }
}
