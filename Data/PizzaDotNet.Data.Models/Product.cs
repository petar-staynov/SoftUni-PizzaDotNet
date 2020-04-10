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
        }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ImageUrl { get; set; }

        public string ImageStorageName { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        // public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
