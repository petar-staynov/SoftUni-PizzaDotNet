namespace PizzaDotNet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Ratings = new HashSet<Rating>();
            // this.Ingredients = new HashSet<Ingredient>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        // [Required] // TODO Enable this
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        /// <summary>
        /// ImageFile will be used to get the image file to be uploaded
        /// MaxFileSize and PermittedExtensions are custom validation attributes used to limit the maximum file size and extensions of the image file.
        /// </summary>
        // [MaxFileSize(1 * 1024 * 1024)] // TODO Enable these
        // [PermittedExtensions(new string[] { ".jpg", ".png", ".gif",".bmp", "jpeg"})]
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }

        /// <summary>
        /// ImageStorageName will be used to keep the name of the object stored in the GCS bucket related to the image file uploaded.
        /// </summary>
        public string ImageStorageName { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        // public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
