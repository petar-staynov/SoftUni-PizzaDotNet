namespace PizzaDotNet.Data.Models
{
    using PizzaDotNet.Data.Common.Models;
    using PizzaDotNet.Data.Models.enums;

    public class Rating : BaseDeletableModel<int>
    {
        public RatingValue Value { get; set; }
    }
}
