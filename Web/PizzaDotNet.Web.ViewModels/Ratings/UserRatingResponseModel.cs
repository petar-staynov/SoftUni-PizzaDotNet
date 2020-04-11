namespace PizzaDotNet.Web.ViewModels.Ratings
{
    public class UserRatingResponseModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public double? Rating { get; set; }
    }
}
