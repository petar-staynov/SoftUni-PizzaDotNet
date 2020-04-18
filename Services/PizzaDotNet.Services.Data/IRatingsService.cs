namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface IRatingsService
    {
        Task RateProductAsync(int productId, string userId, int value);

        double GetProductRating(int productId);

        double GetProductUserRating(int productId, string userId);

        ICollection<ApplicationUser> GetProductVotedUsers(int productId);
    }
}
