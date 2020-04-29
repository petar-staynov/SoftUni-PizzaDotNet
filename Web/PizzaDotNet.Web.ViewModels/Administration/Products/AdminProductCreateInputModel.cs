namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Common.CustomValidationAttributes;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Administration.Shared;
    using PizzaDotNet.Web.ViewModels.Categories;
    using PizzaDotNet.Web.ViewModels.Products;

    public class AdminProductCreateInputModel : IMapFrom<Product>
    {
        public AdminProductCreateInputModel()
        {
            this.Sizes = new List<AdminProductCreateSizeInputModel>();
            this.ImageModel = new ImageUploadInputModel();
        }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "Sizes")]
        [ProductSizes]
        public List<AdminProductCreateSizeInputModel> Sizes { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        public ImageUploadInputModel ImageModel { get; set; }
    }
}
