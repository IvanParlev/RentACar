namespace RentACar.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using RentACar.Data;
    using RentACar.Services.Data;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Location;
    using static DatabaseSeeder;

    public class LocationServiceTests
    {
        private DbContextOptions<RentACarDbContext> dbOptions;
        private RentACarDbContext dbContext;

        private ILocationService locationService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase("RentACarInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RentACarDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.locationService = new LocationService(this.dbContext);


        }

        [Test]
        public async Task LocationExistsByIdShouldReturnTrue()
        {
            int locationId = Location.Id;

            var result = await this.locationService.ExistsByIdAsync(locationId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAddressByIdShouldReturnAddressById()
        {
            var result = await this.locationService.GetAddressById(Location.Id);

            Assert.That(Location.Address, Is.EqualTo(result.Address));
        }

        [Test]
        public async Task ShouldReturnAllAdresses()
        {
            var allAdresses = await this.locationService.AllAddressesAsync();

            Assert.True(allAdresses != null);
        }
    }
}
