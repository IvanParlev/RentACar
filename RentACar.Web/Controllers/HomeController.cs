namespace RentACar.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ILocationService locationService;

        public HomeController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModel =
                await this.locationService.AllAddressesAsync();

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}