namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;

    public interface ICategoriesService
    {
        Task<int> GetCount();

        Task<IEnumerable<T>> GetAll<T>(string sortCriteria = null, int? count = null);

        Task<T> GetById<T>(int categoryId);

        Task<Category> GetBaseById(int categoryId);

        Task<T> GetByName<T>(string name);

        Task<Category> CreateAsync(Category category);

        Task<Category> UpdateAsync(Category category);

        Task<bool> DeleteAsync(Category category);
    }
}
