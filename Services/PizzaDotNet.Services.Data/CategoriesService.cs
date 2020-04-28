namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

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

        public int GetCount()
        {
            int count = this.categoriesRepository
                .All()
                .Count();

            return count;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            var query = this.categoriesRepository.All();
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository
                .All()
                .Where(c => c.Id == id)
                .To<T>()
                .FirstOrDefault();

            return category;
        }

        public T GetByName<T>(string name)
        {
            var category = this.categoriesRepository
                .All()
                .Where(c => c.Name == name)
                .To<T>()
                .FirstOrDefault();

            return category;
        }
    }
}
