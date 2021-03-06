﻿using Microsoft.EntityFrameworkCore;

namespace PizzaDotNet.Services.Data
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
            var rating = await this.ratingsRepository
                .All()
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

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

        public async Task<double> GetProductAverageRating(int productId)
        {
            var query = this.ratingsRepository
                .All()
                .Where(x => x.ProductId == productId);

            if (!query.Any())
            {
                return 0;
            }

            double averageRating = await query.AverageAsync(r => r.Value);

            return averageRating;
        }

        public async Task<double> GetProductUserRating(int productId, string userId)
        {
            var userRating = await this.ratingsRepository
                .All()
                .FirstOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

            if (userRating == null)
            {
                return 0;
            }

            return userRating.Value;
        }
    }
}
