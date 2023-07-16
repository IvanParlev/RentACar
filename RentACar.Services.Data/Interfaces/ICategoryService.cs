namespace RentACar.Services.Data.Interfaces
{
    using RentACar.Web.ViewModels.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<CarSelectCategoryFormModel>> AllCategoriesAsync();

    }
}
