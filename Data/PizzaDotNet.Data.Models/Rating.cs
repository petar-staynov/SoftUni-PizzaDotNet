namespace PizzaDotNet.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using PizzaDotNet.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        [Range(1, 5)]
        public int Value { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
