namespace RentACar.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.Infastructure.Extensions;
    using RentACar.Web.ViewModels.Order;

    using static Common.NotificationMessagesConstants;

    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICarService carService;
        private readonly ILocationService locationService;
        private readonly IOrderService orderService;

        public OrderController(ICarService carService, ILocationService locationService, IOrderService orderService)
        {
            this.carService = carService;
            this.locationService = locationService;
            this.orderService = orderService;
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

            bool isCarRented = await this.carService.IsRentedAsync(id);
            if (isCarRented)
            {
                this.TempData[ErrorMessage] = "Selected car is already rented! Please select another car!";

                return this.RedirectToAction("All", "Car ");
            }

            try
            {
                OrderFormModel formModel = new OrderFormModel()
                {
                    Locations = await this.locationService.AllAddressesForOrderAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();

            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(int id, OrderFormModel formModel)
        {
            bool pickUpLocationExists =
                await this.locationService.ExistsByIdAsync(formModel.PickUpLocationId);
            bool returnLocationExists =
                await this.locationService.ExistsByIdAsync(formModel.ReturnLocationId);
            if (!pickUpLocationExists)
            {
                ModelState.AddModelError(nameof(formModel.PickUpLocationId), "Selected location does not exist!");
            }

            if (!returnLocationExists)
            {
                ModelState.AddModelError(nameof(formModel.ReturnLocationId), "Selected location does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                formModel.Locations = await this.locationService.AllAddressesForOrderAsync();

                return this.View(formModel);
            }

            try
            {
                string renterId = User.GetId()!;
                await this.orderService.CreateOrderAsync(id, formModel, renterId);

                this.TempData[SuccessMessage] = "Your order was added successfuly! Awaits Confirmation!";
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your order!");
                formModel.Locations = await this.locationService.AllAddressesForOrderAsync();
                return this.View(formModel);
            }

            return this.RedirectToAction("Mine", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<OrderDetailsViewModel> myOrders =
                new List<OrderDetailsViewModel>();

            string userId = this.User.GetId()!;

            myOrders.AddRange(await this.orderService.AllByUserIdAsync(userId));

            return this.View(myOrders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            bool orderExists = await this.orderService
                .ExistsByIdAsync(id);
            if (!orderExists)
            {
                this.TempData[ErrorMessage] = "Order with the provided id does not exist!";

                return this.RedirectToAction("Mine", "Order");
            }
            

            OrderDetailsViewModel viewModel = await this.orderService
                .GetOrderDetailsAsync(id);
                

            return this.View(viewModel);
        }


        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occurred!";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
