namespace RentACar.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RentACar.Data;
    using RentACar.Data.Models;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Review;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReviewService : IReviewService
    {
        private readonly RentACarDbContext dbContext;

        public ReviewService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateReviewAsync(ReviewFormModel formModel, int carId, string reviewerId)
        {
            Review newReview = new Review()
            {
                ReviewerId = Guid.Parse(reviewerId),
                CarId = carId,
                Rating = formModel.Rating,
                Comment = formModel.Comment,
            };

            await this.dbContext.Reviews.AddAsync(newReview);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewDetailsViewModel>> GetAllReviewsAsync()
        {
            IEnumerable<ReviewDetailsViewModel> allReviews = await dbContext
                .Reviews
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .Select(r => new ReviewDetailsViewModel()
                {
                    ReviewerId = r.ReviewerId,
                    CarId = r.CarId,
                    Rating = r.Rating,
                    Comment = r.Comment
                })
                .ToArrayAsync();

            return allReviews;


        }

        public async Task<IEnumerable<ReviewDetailsViewModel>> GetReviewsByCarIdAsync(int carId)
        {
            IEnumerable<ReviewDetailsViewModel> allCarReviews = await this.dbContext
                 .Reviews
                 .Where(c => c.CarId == carId)
                 .OrderByDescending(r => r.Id)
                 .Select(c => new ReviewDetailsViewModel()
                 {
                     Comment = c.Comment,
                     Rating = c.Rating
                 })
                 .ToArrayAsync();

            return allCarReviews;
        }


    }

}

