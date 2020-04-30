namespace PizzaDotNet.Web.ViewModels.Administration.Shared
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Common.CustomValidationAttributes;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ImageUploadInputModel : IMapFrom<Product>, IMapFrom<Category>
    {
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Display(Name = "Image")]
        [MaxFileSize(1, "mb")]
        [AllowedExtensions(new string[] { ".jpg", "jpeg", ".png", ".gif", ".bmp" })]
        public IFormFile ImageFile { get; set; }

        public string ImageStorageName { get; set; }
    }
}
