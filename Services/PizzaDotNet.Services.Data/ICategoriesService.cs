namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<int> GetCount();

        Task<IEnumerable<T>> GetAll<T>(int? count = null);

        Task<T> GetByName<T>(string name);
    }
}
