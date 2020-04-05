namespace PizzaDotNet.Data.Models
{
    using PizzaDotNet.Data.Common.Models;
    using PizzaDotNet.Data.Models.Enums;

    public class Rating : BaseDeletableModel<int>
    {
        public RatingValue Value { get; set; }
    }
}
