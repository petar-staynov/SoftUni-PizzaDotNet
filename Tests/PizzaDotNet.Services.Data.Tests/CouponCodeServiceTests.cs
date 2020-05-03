namespace PizzaDotNet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using Xunit;

    public class CouponCodeServiceTests
    {
        [Fact]
        public async Task GetBaseByCodeShouldReturnCouponCode()
        {
            var repository = new Mock<IRepository<CouponCode>>();
            repository.Setup(r => r.All()).Returns(new List<CouponCode>
            {
                new CouponCode() { Code = "AsdAsd" },
                new CouponCode() { Code = "123213" },
                new CouponCode() { Code = "zzzzzz" },
            }.AsQueryable());

            var service = new CouponCodeService(repository.Object);

            var code = await service.GetBaseByCode("AsdAsd");

            Assert.Equal("AsdAsd", code.Code);
        }

        [Fact]
        public async Task CanUseCodeByCodeStringShouldReturnCouponCode()
        {
            var repository = new Mock<IRepository<CouponCode>>();
            repository.Setup(r => r.All()).Returns(new List<CouponCode>
            {
                new CouponCode() { Code = "AsdAsd", IsUsed = true },
                new CouponCode() { Code = "123213", },
                new CouponCode() { Code = "zzzzzz", ValidUntil = DateTime.Now.AddMonths(1) },
            }.AsQueryable());

            var service = new CouponCodeService(repository.Object);

            var resultShouldBeNull1 = await service.CanUseCodeByCodeString("AsdAsd");
            var resultShouldBeNull2 = await service.CanUseCodeByCodeString("123213");
            var resultShouldBeCode = await service.CanUseCodeByCodeString("zzzzzz");

            Assert.Null(resultShouldBeNull1);
            Assert.Null(resultShouldBeNull2);
            Assert.IsType<CouponCode>(resultShouldBeCode);
        }

        [Fact]
        public async Task UseCodeByCodeStringShouldReturnCorrect()
        {
            var repository = new Mock<IRepository<CouponCode>>();
            repository.Setup(r => r.All()).Returns(new List<CouponCode>
            {
                new CouponCode() { Code = "AsdAsd" },
                new CouponCode() { Code = "123213" },
                new CouponCode() { Code = "zzzzzz" },
            }.AsQueryable());

            var service = new CouponCodeService(repository.Object);

            service.UseCodeByCodeString("123213");

            var usedCode = repository.Object.All().FirstOrDefault(c => c.Code == "123213");
            var unusedCode1 = repository.Object.All().FirstOrDefault(c => c.Code == "AsdAsd");
            var unusedCode2 = repository.Object.All().FirstOrDefault(c => c.Code == "zzzzzz");

            Assert.True(usedCode.IsUsed);
            Assert.False(unusedCode1.IsUsed);
            Assert.False(unusedCode2.IsUsed);
        }
    }
}
