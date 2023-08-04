namespace RentACar.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.Infastructure.Extensions;
    using RentACar.Web.ViewModels.Review;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ICarService carService;
        private readonly IReviewService reviewService;

        public ReviewController(ICarService carService, IReviewService reviewService)
        {
            this.carService = carService;
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            bool carExists = await this.carService
                .ExistsByIdAsync(id);
            if (!carExists)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return this.RedirectToAction("All", "Car");
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( int id, ReviewFormModel formModel)
        {
            try
            {
                string userId = User.GetId()!;
                await this.reviewService.CreateReviewAsync(formModel, id, userId);

                this.TempData[SuccessMessage] = "Your review was added successfuly!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your review!");
                
                return this.View(formModel);
            }

            return this.RedirectToAction("Index", "Home");
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occurred!";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
