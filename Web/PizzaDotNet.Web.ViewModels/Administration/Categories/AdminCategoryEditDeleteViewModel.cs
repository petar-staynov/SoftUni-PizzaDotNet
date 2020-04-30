namespace PizzaDotNet.Web.ViewModels.Administration.Categories
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;
    using PizzaDotNet.Web.ViewModels.Administration.Shared;

    public class AdminCategoryEditDeleteViewModel : IMapFrom<Category>
    {
        public AdminCategoryEditDeleteViewModel()
        {
            this.ImageModel = new ImageUploadInputModel();
        }

        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ImageUploadInputModel ImageModel { get; set; }
    }
}
