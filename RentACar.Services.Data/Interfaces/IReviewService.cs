namespace RentACar.Services.Data.Interfaces
{
    using RentACar.Web.ViewModels.Review;

    public interface IReviewService
    {
        Task CreateReviewAsync(ReviewFormModel formModel, int carId, string reviewerId);

        Task<IEnumerable<ReviewDetailsViewModel>> GetAllReviewsAsync();

        Task<IEnumerable<ReviewDetailsViewModel>> GetReviewsByCarIdAsync(int carId);
    }
}
