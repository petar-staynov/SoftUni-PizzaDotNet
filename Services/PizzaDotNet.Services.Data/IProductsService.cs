namespace PizzaDotNet.Services.Data
{
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<int> CreateAsync(string name, string description, decimal price, string imageUrl, int categoryId);

        T GetById<T>(int id);
    }
}
