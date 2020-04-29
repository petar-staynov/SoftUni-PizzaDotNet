namespace PizzaDotNet.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Repositories;
    using PizzaDotNet.Services.Data.Tests.Common;
    using Xunit;

    public class AddressesServiceTests
    {
        [Fact]
        public async Task GetAddressByUserIdShouldGetAddress()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<UserAddress>(dbContext);
            var service = new AddressesService(repository);
            var userManager = MockUserManager.GetUserManager();

            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
            };
            await userManager.CreateAsync(user);

            var userAddress = new UserAddress()
            {
                UserId = user.Id,
            };
            await service.CreateAsync(userAddress);

            var userAddressInvalid = new UserAddress()
            {
                UserId = "none",
            };
            await service.CreateAsync(userAddressInvalid);

            var foundAddress =
                await service.GetBaseByUserId(user.Id);

            Assert.Equal(user.Id, foundAddress.UserId);
        }

        [Fact]
        public async Task CreateAddressShouldCreateAddress()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<UserAddress>(dbContext);
            var service = new AddressesService(repository);
            var userManager = MockUserManager.GetUserManager();

            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
            };
            await userManager.CreateAsync(user);

            var userAddress = new UserAddress()
            {
                UserId = user.Id,
                PersonName = "User",
                Area = "ValidArea",
                Street = "ValidStreet",
                Building = "ValidBuilding",
                Floor = "ValidFloor",
                Apartment = "ValidApartment",
                PhoneNumber = "ValidNumber",
            };
            var userAddress2 = new UserAddress()
            {
                UserId = user.Id,
                PersonName = "",
                Area = "",
                Street = "",
                Building = "",
                Floor = "",
                Apartment = "",
                PhoneNumber = "",
            };

            await service.CreateAsync(userAddress);
            await service.CreateAsync(userAddress2);

            var addressCount = dbContext.Addresses.ToList().Count;
            Assert.Equal(2, addressCount);
        }

        [Fact]
        public async Task UpdateAddressShouldUpdateAddress()
        {
            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfDeletableEntityRepository<UserAddress>(dbContext);
            var service = new AddressesService(repository);
            var userManager = MockUserManager.GetUserManager();

            // Create
            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
            };
            await userManager.CreateAsync(user);

            var userAddress = new UserAddress()
            {
                UserId = user.Id,
                PersonName = "User",
                Area = "ValidArea",
                Street = "ValidStreet",
                Building = "ValidBuilding",
                Floor = "ValidFloor",
                Apartment = "ValidApartment",
                PhoneNumber = "ValidNumber",
            };

            await service.CreateAsync(userAddress);

            // Update
            userAddress.PersonName = "UpdatedUser";
            userAddress.Area = "UpdatedValidArea";
            userAddress.Street = "UpdatedValidStreet";
            userAddress.Building = "UpdatedValidBuilding";
            userAddress.Floor = "UpdatedValidFloor";
            userAddress.Apartment = "UpdatedValidApartment";
            userAddress.PhoneNumber = "UpdatedValidNumber";
            await service.UpdateAsync(userAddress);

            var updatedAddress =
                dbContext.Addresses.ToList().Find(x => x.Id == userAddress.Id);

            Assert.Equal("UpdatedUser", updatedAddress.PersonName);
            Assert.Equal("UpdatedValidArea", updatedAddress.Area);
            Assert.Equal("UpdatedValidStreet", updatedAddress.Street);
            Assert.Equal("UpdatedValidBuilding", updatedAddress.Building);
            Assert.Equal("UpdatedValidFloor", updatedAddress.Floor);
            Assert.Equal("UpdatedValidApartment", updatedAddress.Apartment);
            Assert.Equal("UpdatedValidNumber", updatedAddress.PhoneNumber);
        }
    }
}
