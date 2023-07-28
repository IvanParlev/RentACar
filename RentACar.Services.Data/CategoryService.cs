namespace RentACar.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RentACar.Data;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Category;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly RentACarDbContext dbContext;

        public CategoryService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CarSelectCategoryFormModel>> AllCategoriesAsync()
        {
            IEnumerable<CarSelectCategoryFormModel> allCategories = await this.dbContext
                .Categories
                .AsNoTracking()
                .Select(c => new CarSelectCategoryFormModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToArrayAsync();

            return allCategories;
		}

		public async Task<IEnumerable<string>> AllCategoryNamesAsync()
		{
			IEnumerable<string> allNames = await this.dbContext
				.Categories
				.Select(c => c.Name)
				.ToArrayAsync();

            return allNames;
		}

		public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .Categories
                .AnyAsync(c => c.Id == id);

            return result;
        }
    }
}
