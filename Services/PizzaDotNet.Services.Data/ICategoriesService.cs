namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetById<T>(int id);

        T GetByName<T>(string name);
    }
}
