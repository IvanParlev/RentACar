namespace RentACar.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using RentACar.Data;
    using RentACar.Services.Data;
    using RentACar.Services.Data.Interfaces;

    using static DatabaseSeeder;

    public class CategoryServiceTests
    {
        private DbContextOptions<RentACarDbContext> dbOptions;
        private RentACarDbContext dbContext;

        private ICategoryService categoryService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RentACarDbContext>()
                .UseInMemoryDatabase("RentACarInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RentACarDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.categoryService = new CategoryService(this.dbContext);
        }

        [Test]
        public async Task ShouldReturnAllCategories()
        {
            var allCategories = await this.categoryService.AllCategoriesAsync();

            Assert.That(allCategories != null);
        }

        [Test]
        public async Task ShouldReturnAllCategoriesNames()
        {
            var allCategoryNames = await this.categoryService.AllCategoryNamesAsync();
            
            Assert.That(allCategoryNames != null);
        }

        [Test]
        public async Task CategoryExistsByIdShouldReturnTrueWhenExists()
        {
            bool result = await this.categoryService.ExistsByIdAsync(Category.Id);

            Assert.IsTrue(result);
        }
    }
}
