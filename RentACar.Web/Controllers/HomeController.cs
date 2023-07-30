namespace RentACar.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Car;
    using RentACar.Web.ViewModels.Home;
    using RentACar.Web.ViewModels.Location;
    using RentACar.Web.ViewModels.Review;

    public class HomeController : Controller
    {
        private readonly ILocationService locationService;
        private readonly ICarService carService;
        private readonly IReviewService reviewService;

        public HomeController(ILocationService locationService, ICarService carService, IReviewService reviewService)
        {
            this.locationService = locationService;
            this.carService = carService;
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