namespace RentACar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Review;

    public class HomeController : Controller
    {
        private readonly IReviewService reviewService;

        public HomeController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ReviewDetailsViewModel> reviewModel =
                await this.reviewService.GetAllReviewsAsync();

            return View(reviewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return this.View("Error404");
            }

            return View();
        }
    }
}