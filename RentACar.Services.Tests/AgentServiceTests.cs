namespace RentACar.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using RentACar.Data;
    using RentACar.Services.Data;
    using RentACar.Services.Data.Interfaces;

    using static DatabaseSeeder;

    public class AgentServiceTests
    {
        private DbContextOptions<RentACarDbContext> dbOptions;
        private RentACarDbContext dbContext;

        private IAgentService agentService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase("RentACarInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RentACarDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.agentService = new AgentService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AgentExistsByUserIdAsyncShouldReturnTrueWhenExists()
        {
            string existingAgentUserId = AgentUser.Id.ToString();

            bool result = await this.agentService.AgentExistsByUserIdAsync(existingAgentUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task AgentExistsByUserIdAsyncShouldReturnFalseWhenNotExists()
        {
            string existingAgentUserId = RenterUser.Id.ToString();

            bool result = await this.agentService.AgentExistsByUserIdAsync(existingAgentUserId);

            Assert.IsFalse(result);
        }
    }
}