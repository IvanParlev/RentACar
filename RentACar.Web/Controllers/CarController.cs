namespace RentACar.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using RentACar.Services.Data.Interfaces;
	using RentACar.Services.Data.Models.Car;
	using RentACar.Web.Infastructure.Extensions;
    using RentACar.Web.ViewModels.Car;

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
        public async Task<IActionResult> All([FromQuery]AllCarsQueryModel queryModel)
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

                throw;
            }


        }

        [HttpPost]
        public async Task<IActionResult> Add(CarFormModel formModel)
        {
            bool isAgent =
                await this.agentService.AgentExistsByUserIdAsync(this.User.GetId()!);
            if (!isAgent)
            {
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

                await this.carService.CreateAsync(formModel, agentId!);
            }
            catch (Exception _)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add you new car!");
                formModel.Categories = await this.categoryService.AllCategoriesAsync();
                return this.View(formModel);
            }

            return this.RedirectToAction("All", "Car");
        }
    }
}
