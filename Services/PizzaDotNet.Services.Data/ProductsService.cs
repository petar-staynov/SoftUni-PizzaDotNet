﻿using PizzaDotNet.Common;

namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public int GetCount()
        {
            int count = this.productsRepository
                .All()
                .Count();

            return count;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            this.productsRepository.Update(product);
            await this.productsRepository.SaveChangesAsync();

            return product;
        }

        public Task<bool> DeleteAsync(int orderId)
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            var product = this.productsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return product;
        }

        public Product GetBaseById(int id)
        {
            var product = this.productsRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            return product;
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId, string sortCriteria = null, int? count = null)
        {
            var categoryProductsQuery = this.productsRepository
                .All()
                .Where(p => p.CategoryId == categoryId);

            switch (sortCriteria)
            {
                case SortingCriterias.PRODUCT_PRICE_LOWEST_TO_HIGHEST:
                    categoryProductsQuery = categoryProductsQuery.OrderBy(p => p.Sizes.Select(s => s.Price).Sum());
                    break;
                case SortingCriterias.PRODUCT_PRICE_HIGHEST_TO_LOWEST:
                    categoryProductsQuery = categoryProductsQuery.OrderByDescending(p => p.Sizes.Select(s => s.Price).Sum());
                    break;
                case SortingCriterias.PRODUCT_RATING_LOWEST_TO_HIGHEST:
                    categoryProductsQuery = categoryProductsQuery.OrderBy(p => p.Ratings.Select(r => r.Value).Average());
                    break;
                case SortingCriterias.PRODUCT_RATING_HIGHEST_TO_LOWEST:
                    categoryProductsQuery = categoryProductsQuery.OrderByDescending(p => p.Ratings.Select(r => r.Value).Average());
                    break;
                case SortingCriterias.PRODUCT_NAME_ASCENDING:
                    categoryProductsQuery = categoryProductsQuery.OrderBy(p => p.Name);
                    break;
                case SortingCriterias.PRODUCT_NAME_DESCENDING:
                    categoryProductsQuery = categoryProductsQuery.OrderByDescending(p => p.Name);
                    break;
            }

            var categoryProducts = categoryProductsQuery.To<T>().ToList();

            return categoryProducts;
        }

        public IEnumerable<T> GetAll<T>(string sortCriteria = null, int? count = null)
        {
            var productsQuery = this.productsRepository
                .All();

            switch (sortCriteria)
            {
                case SortingCriterias.PRODUCT_PRICE_LOWEST_TO_HIGHEST:
                    productsQuery = productsQuery.OrderBy(p => p.Sizes.Select(s => s.Price).Sum());
                    break;
                case SortingCriterias.PRODUCT_PRICE_HIGHEST_TO_LOWEST:
                    productsQuery = productsQuery.OrderByDescending(p => p.Sizes.Select(s => s.Price).Sum());
                    break;
                case SortingCriterias.PRODUCT_RATING_LOWEST_TO_HIGHEST:
                    productsQuery = productsQuery.OrderBy(p => p.Ratings.Select(r => r.Value).Average());
                    break;
                case SortingCriterias.PRODUCT_RATING_HIGHEST_TO_LOWEST:
                    productsQuery = productsQuery.OrderByDescending(p => p.Ratings.Select(r => r.Value).Average());
                    break;
                case SortingCriterias.PRODUCT_NAME_ASCENDING:
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
                case SortingCriterias.PRODUCT_NAME_DESCENDING:
                    productsQuery = productsQuery.OrderByDescending(p => p.Name);
                    break;
                case SortingCriterias.PRODUCT_CATEGORY_NAME_ASCENDING:
                    productsQuery = productsQuery.OrderBy(p => p.Category.Name);
                    break;
                case SortingCriterias.PRODUCT_CATEGORY_NAME_DESCENDING:
                    productsQuery = productsQuery.OrderByDescending(p => p.Category.Name);
                    break;
            }

            var products = productsQuery.To<T>().ToList();

            return products;
        }
    }
}
