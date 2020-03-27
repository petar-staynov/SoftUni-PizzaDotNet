namespace PizzaDotNet.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using PizzaDotNet.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    
        // public virtual ICollection<Rating> Ratings { get; set; }
    }
}
