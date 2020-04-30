using PizzaDotNet.Common;

namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<int> GetCount()
        {
            int count = this.categoriesRepository
                .All()
                .Count();

            return count;
        }

        public async Task<IEnumerable<T>> GetAll<T>(string sortCriteria = null, int? count = null)
        {
            var query = this.categoriesRepository
                .All();

            switch (sortCriteria)
            {
                case SortingCriterias.CATEGORIES_PRODUCT_COUNT_HIGHEST_TO_LOWEST:
                    query = query.OrderByDescending(c => c.Products.Count());
                    break;
                case SortingCriterias.CATEGORIES_PRODUCT_COUNT_LOWEST_TO_HIGHEST:
                    query = query.OrderBy(c => c.Products.Count());
                    break;
                case SortingCriterias.CATEGORIES_NAME_ASCENDING:
                    query = query.OrderBy(c => c.Name);
                    break;
                case SortingCriterias.CATEGORIES_NAME_DESCENDING:
                    query = query.OrderByDescending(c => c.Name);
                    break;
            }

            if (typeof(T) == typeof(Category))
            {
                var categories = query.ToList<Category>();
                return categories as IEnumerable<T>;
            }

            var result = await query.To<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetById<T>(int categoryId)
        {
            var category = await this.categoriesRepository
                .All()
                .Where(c => c.Id == categoryId)
                .To<T>()
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<Category> GetBaseById(int categoryId)
        {
            var category = await this.categoriesRepository
                .All()
                .Where(c => c.Id == categoryId)
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<T> GetByName<T>(string name)
        {
            var category = await this.categoriesRepository
                .All()
                .Where(c => c.Name == name)
                .To<T>()
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();

            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            this.categoriesRepository.Update(category);
            await this.categoriesRepository.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            this.categoriesRepository.Delete(category);
            var result = await this.categoriesRepository.SaveChangesAsync();

            return result > 0;
        }
    }
}
