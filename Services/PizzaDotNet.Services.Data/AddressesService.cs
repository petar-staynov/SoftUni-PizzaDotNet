using System.Threading.Tasks;

namespace PizzaDotNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Mapping;

    public class AddressesService : IAddressesService
    {
        private readonly IRepository<UserAddress> addressesRepository;

        public AddressesService(IRepository<UserAddress> addressesRepository)
        {
            this.addressesRepository = addressesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            var query = this.addressesRepository.All();

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByUserId<T>(string id)
        {
            var address = this.addressesRepository
                .All()
                .Where(a => a.UserId == id)
                .To<T>()
                .FirstOrDefault();

            return address;
        }

        public T GetById<T>(int id)
        {
            var address = this.addressesRepository
                .All()
                .Where(a => a.Id == id)
                .To<T>()
                .FirstOrDefault();

            return address;
        }

        public async Task<UserAddress> CreateAddressAsync(UserAddress address)
        {
            await this.addressesRepository.AddAsync(address);
            await this.addressesRepository.SaveChangesAsync();

            return address;
        }

        public async Task<UserAddress> UpdateAddressAsync(UserAddress address)
        {
            this.addressesRepository.Update(address);
            await this.addressesRepository.SaveChangesAsync();

            return address;
        }
    }
}
