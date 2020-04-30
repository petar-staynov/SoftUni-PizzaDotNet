namespace PizzaDotNet.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Administration.Shared;

    public class AdminCategoryEditInputModel : IMapFrom<Category>
    {
        public AdminCategoryEditInputModel()
        {
            this.ImageModel = new ImageUploadInputModel();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public ImageUploadInputModel ImageModel { get; set; }

        [Display(Name = "Upload new  image?")]
        public bool IsNewImage { get; set; }
    }
}
