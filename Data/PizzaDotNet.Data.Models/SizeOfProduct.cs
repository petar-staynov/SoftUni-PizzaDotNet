namespace PizzaDotNet.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PizzaDotNet.Data.Common.Models;

    public class SizeOfProduct : BaseDeletableModel<int>
    {
        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal Price { get; set; }
    }
}
