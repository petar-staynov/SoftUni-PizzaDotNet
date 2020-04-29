namespace PizzaDotNet.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Common;
    using PizzaDotNet.Data.Common.CustomValidationAttributes;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Administration.Shared;
    using PizzaDotNet.Web.ViewModels.Categories;
    using PizzaDotNet.Web.ViewModels.ProductSize;

    public class AdminProductEditInputModel : IMapFrom<Product>
    {
        public AdminProductEditInputModel()
        {
            this.Sizes = new List<AdminProductSizeInputModel>(GlobalConstants.MaxNumberOfProductSizes);
            this.ImageModel = new ImageUploadInputModel();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public IEnumerable<CategoryDropdownViewModel> Categories { get; set; }

        public ImageUploadInputModel ImageModel { get; set; }

        [Display(Name = "Upload new  image?")]
        public bool IsNewImage { get; set; }

        [Display(Name = "Sizes")]
        [ProductSizes]
        public List<AdminProductSizeInputModel> Sizes { get; set; }

        public virtual ICollection<AdminProductRatingViewModel> Ratings { get; set; }

        [Display(Name = "Rating (average)")]
        public double? RatingAverage => this.Ratings != null && this.Ratings.Count > 0
            ? this.Ratings.Average(r => r.Value)
            : 0;
    }
}
