namespace RentACar.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Services.Data.Models.Car;
    using RentACar.Web.Infastructure.Extensions;
    using RentACar.Web.ViewModels.Car;

    using static Common.NotificationMessagesConstants;
    using static Common.ApplicationConstants;

    [Authorize]
    public class CarController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        private readonly ICarService carService;
        private readonly IOrderService orderService;
        private readonly IMemoryCache memoryCache;

        public CarController(ICategoryService categoryService, IAgentService agentService, ICarService carService, IOrderService orderService, IMemoryCache memoryCache)
        {
            this.categoryService = categoryService;
            this.agentService = agentService;
            this.carService = carService;
            this.orderService = orderService;
            this.memoryCache = memoryCache;
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
            if (!isUserAgent && !this.User.IsAdmin())
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
            if (!isUserAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to edit car info!";

                return this.RedirectToAction("Become", "Agent");
            }

            try
            {
                await this.carService.EditCarByIdAndFormModelAsync(id, formModel);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to update the car information!");
                formModel.Categories = await this.categoryService.AllCategoriesAsync();

                return this.View(formModel);
            }

            this.TempData[SuccessMessage] = "Car information was updated successfully!";
            return this.RedirectToAction("Details", "Car", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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
            if (!isUserAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to delete a car!";

                return this.RedirectToAction("Become", "Agent");
            }

            try
            {
                CarDeleteDetailsViewModel viewModel =
                    await this.carService.GetCarForDeleteByIdAsync(id);

                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CarDeleteDetailsViewModel model)
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
            if (!isUserAgent && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an agent in order to delete a car!";

                return this.RedirectToAction("Become", "Agent");
            }

            try
            {
                await this.carService.DeleteCarByIdAsync(id);

                this.TempData[WarningMessage] = "Car was successfully deleted!";

                return this.RedirectToAction("All", "Car");
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Rent(string id)
        {
            bool orderExists = await this.orderService.ExistsByIdAsync(id);
            if (!orderExists)
            {
                this.TempData[ErrorMessage] = "Order with the provided id does not exist!";

                return this.RedirectToAction("Mine", "Order");
            }

            try
            {
                await this.carService.RentCarAsync(id);
            }
            catch (Exception)
            {
                return this.GeneralError();
                
            }

            this.memoryCache.Remove(OrdersCacheKey);    

            return this.RedirectToAction("Mine", "Order");
        }

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occurred!";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
