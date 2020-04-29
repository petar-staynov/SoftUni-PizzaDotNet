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
        private readonly IRepository<Category> categoriesRepository;

        public CategoriesService(IRepository<Category> categoriesRepository)
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

        public async Task<IEnumerable<T>> GetAll<T>(int? count = null)
        {
            var query = this.categoriesRepository.All();
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            if (typeof(T) == typeof(Category))
            {
                var categories = query.ToList<Category>();
                return categories as IEnumerable<T>;
            }

            var result = await query.To<T>().ToListAsync();
            return result;
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
    }
}
