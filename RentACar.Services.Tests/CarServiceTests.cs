namespace RentACar.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using RentACar.Data;
    using RentACar.Services.Data;
    using RentACar.Services.Data.Interfaces;

    using static DatabaseSeeder;

    public class CarServiceTests
    {
        private DbContextOptions<RentACarDbContext> dbOptions;
        private RentACarDbContext dbContext;

        private ICarService carService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase("RentACarInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RentACarDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.carService = new CarService(this.dbContext);
        }

        [Test]
        public async Task DeleteCarByIdShouldReturnIsActiveFalse()
        {
            //CarToDelete is seeded only for this Test.
            await this.carService.DeleteCarByIdAsync(CarToDelete.Id);

            Assert.IsFalse(CarToDelete.IsActive);
            
        }

        [Test]
        public async Task ExistsByIdShouldReturnTrueIfExists()
        {
            var result = await this.carService.ExistsByIdAsync(Car.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdShouldReturnFalseIfNotExists()
        {
            // Id = 69 doesn't exist
            var result = await this.carService.ExistsByIdAsync(69);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetCarForDeleteByIdAsyncShouldReturnCar()
        {
            var car = await this.carService.GetCarForDeleteByIdAsync(Car.Id);

            Assert.AreEqual(Car.Model, car.CarModel);
        }

        [Test]
        public async Task GetCarForEditByIdAsyncShouldReturnCar()
        {
            var car = await this.carService.GetCarForEditByIdAsync(Car.Id);

            Assert.AreEqual(Car.Model, car.Model);
        }

        [Test]
        public async Task GetDetailsByIdAsyncShouldReturnDetaisOfCar()
        {
            var car = await this.carService.GetDetailsByIdAsync(Car.Id);

            Assert.AreEqual(Car.Id, car.Id);
        }

        [Test]
        public async Task IsRentedByIdShouldReturnTrueIfIsRented()
        {
            var result = await this.carService.IsRentedAsync(Car.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsRentedByIdShouldReturnFalsIfNotRented()
        {
            // Id = 1 is seeded car with no renter.
            var result = await this.carService.IsRentedAsync(1);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task RentCarByOrderIdShouldReturnRenterIdWithValue()
        {
            // CarToRent is only used for this Test
            await this.carService.RentCarAsync(Order.Id.ToString());

            Assert.That(CarToRent.RenterId != null);
        }
    }
}
