namespace RentACar.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.Data;
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
                    Name = c.Name
                })
                .ToArrayAsync();

            return allCategories;
        }
    }
}
