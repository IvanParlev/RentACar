namespace RentACar.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Services.Data.Models.Car;
    using RentACar.Web.Infastructure.Extensions;
    using RentACar.Web.ViewModels.Car;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class CarController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        private readonly ICarService carService;

        public CarController(ICategoryService categoryService, IAgentService agentService, ICarService carService)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.carService = carService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllCarsQueryModel queryModel)
        {
            AllCarsFilteredAndPagedServiceModel serviceModel =
                await this.carService.AllAsync(queryModel);

            queryModel.Cars = serviceModel.Cars;
            queryModel.TotalCars = serviceModel.TotalCarsCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new cars!";

                return this.RedirectToAction("Become", "Agent");
            }
            try
            {
                CarFormModel formModel = new CarFormModel()
                {
                    Categories = await this.categoryService.AllCategoriesAsync()
                };

                return View(formModel);

            }
            catch (Exception)
            {
                return this.GeneralError();
            }


        }

        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel formModel)
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to add new cars!";

                return this.RedirectToAction("Become", "Agent");
            }

            bool categoryExists =
                await this.categoryService.ExistsByIdAsync(formModel.CategoryId);

            if (!categoryExists)
            {
                this.ModelState.AddModelError(nameof(formModel.CategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }

            try
            {
                string? agentId =
                   await this.agentService.GetAgentIdByUserIdAsync(this.User.GetId()!);

                int carId =
                     await this.carService.CreateAndReturnIdAsync(formModel, agentId!);

                this.TempData[SuccessMessage] = "Car was added successfully!";

                return this.RedirectToAction("Details", "Car", new { id = carId });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add you new car!");
                formModel.Categories = await this.categoryService.AllCategoriesAsync();
                return this.View(formModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            bool carExists = await this.carService
                .ExistsByIdAsync(id);
            if (!carExists)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return this.RedirectToAction("All", "Car");
            }
            CarDetailsViewModel viewModel = await this.carService
                .GetDetailsByIdAsync(id);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool carExists = await this.carService
                .ExistsByIdAsync(id);
            if (!carExists)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return this.RedirectToAction("All", "Car");
            }

            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit car info!";

                return this.RedirectToAction("Become", "Agent");
            }
            try
            {
                CarFormModel formModel = await this.carService
               .GetCarForEditByIdAsync(id);
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CarFormModel formModel)
        {
            if (!this.ModelState.IsValid)
            {
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }

            bool carExists = await this.carService
                .ExistsByIdAsync(id);
            if (!carExists)
            {
                this.TempData[ErrorMessage] = "Car with the provided id does not exist!";

                return this.RedirectToAction("All", "Car");
            }

            bool isUserAgent = await this.agentService
                .AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserAgent)
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit car info!";

                return this.RedirectToAction("Become", "Agent");
            }

            try
            {
                await this.carService.EditCarByIdAndFormModel(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to update the car information!");
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }

            return this.RedirectToAction("Details", "Car", new { id });
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occurred!";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
