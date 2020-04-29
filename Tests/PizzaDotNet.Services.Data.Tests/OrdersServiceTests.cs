namespace PizzaDotNet.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using PizzaDotNet.Data.Common.Repositories;
    using PizzaDotNet.Data.Models;
    using PizzaDotNet.Data.Repositories;
    using PizzaDotNet.Services.Data.Tests.Common;
    using Xunit;

    public class OrdersServiceTests
    {
        [Fact]
        public async Task GetCountShouldReturnCount()
        {
            var ordersRepository = new Mock<IRepository<Order>>();
            ordersRepository.Setup(r => r.All()).Returns(new List<Order>
            {
                new Order() { TotalPriceDiscounted = 5M },
                new Order() { TotalPriceDiscounted = 10M },
                new Order() { TotalPriceDiscounted = 20M },
            }.AsQueryable());

            var ordersStatusRepository = new Mock<IRepository<OrderStatus>>();
            ordersStatusRepository.Setup(r => r.All()).Returns(new List<OrderStatus>
            {
                new OrderStatus() { Id = 1, Status = "Processing" },
                new OrderStatus() { Id = 1, Status = "Shipped" },
                new OrderStatus() { Id = 1, Status = "Cancelled" },
            }.AsQueryable());

            var orderStatusService = new OrderStatusService(ordersStatusRepository.Object);
            var ordersService = new OrdersService(ordersRepository.Object, orderStatusService);

            var count = await ordersService.GetCount();

            Assert.Equal(3, count);
        }

        [Fact]
        public async Task GetTotalProfitShouldReturnCorrect()
        {
            decimal price1 = 5M;
            decimal price2 = 10M;
            decimal price3 = 15M;

            var ordersRepository = new Mock<IRepository<Order>>();
            ordersRepository.Setup(r => r.All()).Returns(new List<Order>
            {
                new Order() { TotalPriceDiscounted = price1 },
                new Order() { TotalPriceDiscounted = price2 },
                new Order() { TotalPriceDiscounted = price3 },
            }.AsQueryable());

            var orderStatusMockService = new Mock<IOrderStatusService>();

            var ordersService = new OrdersService(ordersRepository.Object, orderStatusMockService.Object);

            var totalProfits = await ordersService.GetTotalProfit();

            Assert.Equal(price1 + price2 + price3, totalProfits);
        }

        [Fact]
        public async Task CreateAsyncShouldCreateOrder()
        {
            var orderStatusMockService = new Mock<IOrderStatusService>();

            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfRepository<Order>(dbContext);
            var ordersService = new OrdersService(repository, orderStatusMockService.Object);
            var userManager = MockUserManager.GetUserManager();

            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
            };
            await userManager.CreateAsync(user);

            var order1 = new Order()
            {
                UserId = user.Id,
                User = user,
                OrderAddress = new OrderAddress() { },
                OrderStatusId = 5,
                OrderStatus = new OrderStatus() { Status = "Processing" },
                OrderProducts = new List<OrderProduct>(),
                CouponCodeId = null,
                CouponCode = null,
                TotalPrice = 20,
                TotalPriceDiscounted = 20,
                OrderNotes = null,
            };

            await ordersService.CreateAsync(order1);

            var orders = dbContext.Orders.ToList();
            var ordersCount = orders.Count;
            Assert.Equal(1, ordersCount);
            Assert.Equal(orders.First().UserId, user.Id);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateOrder()
        {
            var orderStatusMockService = new Mock<IOrderStatusService>();

            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfRepository<Order>(dbContext);
            var ordersService = new OrdersService(repository, orderStatusMockService.Object);
            var userManager = MockUserManager.GetUserManager();

            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
            };
            await userManager.CreateAsync(user);

            // Create
            var order1 = new Order()
            {
                UserId = user.Id,
                User = user,
                OrderAddress = new OrderAddress() { },
                OrderStatusId = 5,
                OrderStatus = new OrderStatus() { Status = "Processing" },
                OrderProducts = new List<OrderProduct>(),
                CouponCodeId = null,
                CouponCode = null,
                TotalPrice = 20,
                TotalPriceDiscounted = 20,
                OrderNotes = null,
            };
            await ordersService.CreateAsync(order1);

            // Update
            order1.UserId = "2";
            order1.OrderStatusId = 4;
            order1.CouponCodeId = 5;
            order1.TotalPrice = 10;
            order1.TotalPriceDiscounted = 8;
            order1.OrderNotes = "Notes";
            await ordersService.UpdateAsync(order1);

            // Get
            var orderFromDb = dbContext.Orders.FirstOrDefault(o => o.Id == order1.Id);

            Assert.Equal("2", orderFromDb.UserId);
            Assert.Equal(4, orderFromDb.OrderStatusId);
            Assert.Equal(5, orderFromDb.CouponCodeId);
            Assert.Equal(10, orderFromDb.TotalPrice);
            Assert.Equal(8, orderFromDb.TotalPriceDiscounted);
            Assert.Equal("Notes", orderFromDb.OrderNotes);
        }

        [Fact]
        public void UpdateShouldUpdateOrder()
        {
            var orderStatusMockService = new Mock<IOrderStatusService>();

            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfRepository<Order>(dbContext);
            var ordersService = new OrdersService(repository, orderStatusMockService.Object);
            var userManager = MockUserManager.GetUserManager();

            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
            };
            userManager.CreateAsync(user);

            // Create
            var order1 = new Order()
            {
                UserId = user.Id,
                User = user,
                OrderAddress = new OrderAddress() { },
                OrderStatusId = 5,
                OrderStatus = new OrderStatus() { Status = "Processing" },
                OrderProducts = new List<OrderProduct>(),
                CouponCodeId = null,
                CouponCode = null,
                TotalPrice = 20,
                TotalPriceDiscounted = 20,
                OrderNotes = null,
            };
            ordersService.CreateAsync(order1);

            // Update
            order1.UserId = "2";
            order1.OrderStatusId = 4;
            order1.CouponCodeId = 5;
            order1.TotalPrice = 10;
            order1.TotalPriceDiscounted = 8;
            order1.OrderNotes = "Notes";
            ordersService.Update(order1);

            // Get
            var orderFromDb = dbContext.Orders.FirstOrDefault(o => o.Id == order1.Id);

            Assert.Equal("2", orderFromDb.UserId);
            Assert.Equal(4, orderFromDb.OrderStatusId);
            Assert.Equal(5, orderFromDb.CouponCodeId);
            Assert.Equal(10, orderFromDb.TotalPrice);
            Assert.Equal(8, orderFromDb.TotalPriceDiscounted);
            Assert.Equal("Notes", orderFromDb.OrderNotes);
        }

        [Fact]
        public async Task GetBaseByIdShouldReturnOrder()
        {
            var orderStatusMockService = new Mock<IOrderStatusService>();

            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();
            var repository = new EfRepository<Order>(dbContext);
            var ordersService = new OrdersService(repository, orderStatusMockService.Object);
            var userManager = MockUserManager.GetUserManager();

            var user = new ApplicationUser
            {
                Email = "user@mail.com",
                UserName = "user@mail.com",
            };
            await userManager.CreateAsync(user);

            // Create
            var order = new Order()
            {
                UserId = user.Id,
                User = user,
                OrderProducts = new List<OrderProduct>(),
                OrderAddress = new OrderAddress(),
                OrderStatus = new OrderStatus(),
            };
            await ordersService.CreateAsync(order);

            // Get
            var foundOrder = await ordersService.GetBaseById(order.Id);

            Assert.Equal(user.Id, foundOrder.UserId);
        }

        [Fact]
        public async Task GetByUserIdShouldReturnCorrectOrders()
        {
            var orderStatusMockService = new Mock<IOrderStatusService>();

            var repository = new Mock<IRepository<Order>>();
            repository.Setup(r => r.All()).Returns(new List<Order>
            {
                new Order() { UserId = "1" },
                new Order() { UserId = "2" },
                new Order() { UserId = "3" },
            }.AsQueryable());

            var service = new OrdersService(repository.Object, orderStatusMockService.Object);

            var categoriesFromService = await service.GetByUserId<Order>("1");
            var count = categoriesFromService.Count();

            Assert.Equal(1, count);
            Assert.Equal("1", categoriesFromService.First().UserId);
        }

        [Fact]
        public async Task GetAllShouldReturnAllOrders()
        {
            var orderStatusMockService = new Mock<IOrderStatusService>();

            var repository = new Mock<IRepository<Order>>();
            repository.Setup(r => r.All()).Returns(new List<Order>
            {
                new Order(),
                new Order(),
                new Order(),
            }.AsQueryable());

            var service = new OrdersService(repository.Object, orderStatusMockService.Object);

            var categories = await service.GetAll<Order>();
            var count = categories.Count();

            Assert.Equal(3, count);
        }


        [Fact]
        public async Task ChangingOrderStatusShouldChangeStatus()
        {
            const string finalStatus = "Shipped";

            var dbContext = ApplicationDbContextInMemoryFactory.InitializeContext();

            var orderStatusRepository = new EfRepository<OrderStatus>(dbContext);
            var orderStatusService = new OrderStatusService(orderStatusRepository);

            var orderRepository = new EfRepository<Order>(dbContext);
            var ordersService = new OrdersService(orderRepository, orderStatusService);

            // Create
            var order = new Order()
            {
                OrderStatus = new OrderStatus() { Status = "Processing" },
            };
            var order2 = new Order()
            {
                OrderStatus = new OrderStatus() { Status = "Processing" },
            };
            await ordersService.CreateAsync(order);
            await ordersService.CreateAsync(order2);

            // Update
            order.OrderStatus = new OrderStatus() { Status = finalStatus };
            await ordersService.UpdateAsync(order);

            // Get
            var foundOrder = dbContext.Orders.FirstOrDefault(o => o.Id == order.Id);

            Assert.Equal(foundOrder.OrderStatus.Status, finalStatus);
        }
    }
}
