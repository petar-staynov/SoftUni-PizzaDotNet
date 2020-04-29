namespace PizzaDotNet.Services.Data.Tests.Common
{
    using System.Reflection;

    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Services.Data.Tests.Models;
    using PizzaDotNet.Services.Mapping;

    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(CategoryServiceModel).GetTypeInfo().Assembly,
                typeof(Category).GetTypeInfo().Assembly);
        }
    }
}
