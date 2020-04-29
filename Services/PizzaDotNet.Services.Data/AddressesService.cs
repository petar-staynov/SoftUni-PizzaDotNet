namespace PizzaDotNet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task<T> GetByUserId<T>(string id)
        {
            var query = this.addressesRepository
                .All()
                .Where(a => a.UserId == id);

            var address = await query.To<T>().FirstOrDefaultAsync();

            return address;
        }

        public async Task<UserAddress> GetBaseByUserId(string id)
        {
            var address = await this.addressesRepository
                .All()
                .FirstOrDefaultAsync(a => a.UserId == id);

            return address;
        }

        public async Task<UserAddress> CreateAsync(UserAddress address)
        {
            await this.addressesRepository.AddAsync(address);
            await this.addressesRepository.SaveChangesAsync();

            return address;
        }

        public async Task<UserAddress> UpdateAsync(UserAddress address)
        {
            this.addressesRepository.Update(address);
            await this.addressesRepository.SaveChangesAsync();

            return address;
        }
    }
}
