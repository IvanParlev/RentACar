namespace RentACar.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using RentACar.Data;
    using RentACar.Services.Data;
    using RentACar.Services.Data.Interfaces;

    using static DatabaseSeeder;

    public class ReviewServiceTests
    {
        private DbContextOptions<RentACarDbContext> dbOptions;
        private RentACarDbContext dbContext;

        private IReviewService reviewService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase("RentACarInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RentACarDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.reviewService = new ReviewService(this.dbContext);
        }

        [Test]
        public async Task ShouldReturnAllReviews()
        {
            var allReviews = await this.reviewService.GetAllReviewsAsync();

            Assert.AreNotEqual(allReviews, null);
        }

        [Test]
        public async Task GetReviewsByCarIdShouldReturnReviewsOfCar()
        {    
            var allReviews = await this.reviewService.GetReviewsByCarIdAsync(Review.CarId);

            Assert.AreNotEqual(allReviews, null);
        }

        
    }
}
