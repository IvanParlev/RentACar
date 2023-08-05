namespace RentACar.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Order;

    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<OrderViewModel> allOrders = 
                await this.orderService.AllAsync();

            return this.View(allOrders);
        }
    }
}
