namespace RentACar.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using RentACar.Data;
    using RentACar.Services.Data;
    using RentACar.Services.Data.Interfaces;

    using static DatabaseSeeder;

    public class OrderServiceTests
    {
        private DbContextOptions<RentACarDbContext> dbOptions;
        private RentACarDbContext dbContext;

        private IOrderService orderService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase("RentACarInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RentACarDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.orderService = new OrderService(this.dbContext);
        }

        [Test]
        public async Task ShouldReturnAllOrdersByUserId()
        {
            var allUserOrders = await this.orderService.AllByUserIdAsync(RenterUser.Id.ToString());

           Assert.IsTrue(allUserOrders.Any());
        }

        [Test]
        public async Task ExistsByOrderIdShouldReturnTrueIfExists()
        {
            var result = await this.orderService.ExistsByIdAsync(Order.Id.ToString());

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByOrderIdShouldReturnFalseIfNotExists()
        {
            var result = await this.orderService.ExistsByIdAsync("3e678920-3205-42a6-9866-1b66e1acfa5e");

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ShouldReturnOrderDetailsById()
        {
            var order = await this.orderService.GetOrderDetailsAsync(Order.Id.ToString());

            Assert.That(order, Is.Not.Null);
        }

    }
}
