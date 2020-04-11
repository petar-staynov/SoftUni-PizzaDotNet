﻿namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;

    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rating> ratingsRepository;

        public RatingsService(IRepository<Rating> ratingsRepository)
        {
            this.ratingsRepository = ratingsRepository;
        }

        public async Task RateProductAsync(int productId, string userId, int value)
        {
            // TODO maybe add check for 1-5 range of rating value
            var rating = this.ratingsRepository
                .All()
                .FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);

            if (rating != null)
            {
                rating.Value = value;
            }
            else
            {
                rating = new Rating
                {
                    Value = value,
                    UserId = userId,
                    ProductId = productId,
                };

                await this.ratingsRepository.AddAsync(rating);
            }

            await this.ratingsRepository.SaveChangesAsync();
        }

        public double? GetProductRating(int productId)
        {
            var query = this.ratingsRepository
                .All()
                .Where(x => x.ProductId == productId);

            if (!query.Any())
            {
                return null;
            }

            double averageRating = query.Average(r => r.Value);

            return averageRating;
        }

        public double? GetProductUserRating(int productId, string userId)
        {
            var userRating = this.ratingsRepository
                .All()
                .FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);

            if (userRating == null)
            {
                return null;
            }

            return userRating.Value;
        }

        public ICollection<ApplicationUser> GetProductVotedUsers(int productId)
        {
            throw new System.NotImplementedException();
        }
    }
}
