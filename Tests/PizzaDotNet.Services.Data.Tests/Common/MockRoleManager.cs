namespace PizzaDotNet.Services.Data.Tests.Common
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Moq;

    public static class MockRoleManager
    {
        public static RoleManager<IdentityRole> GetMockRoleManager()
        {
            var mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

            return mockRoleManager.Object;
        }
    }
}