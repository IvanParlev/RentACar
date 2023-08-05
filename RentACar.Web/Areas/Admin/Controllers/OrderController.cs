namespace RentACar.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Order;

    using static Common.ApplicationConstants;

    public class OrderController : BaseAdminController
    {
        private readonly IOrderService orderService;
        private readonly IMemoryCache memoryCache;

        public OrderController(IOrderService orderService, IMemoryCache memoryCache)
        {
            this.orderService = orderService;
            this.memoryCache = memoryCache;
        }

        [Route("Order/All")]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> All()
        {
            IEnumerable<OrderViewModel> allOrders =
               this.memoryCache.Get<IEnumerable<OrderViewModel>>(OrdersCacheKey);
            if (allOrders == null)
            {
                allOrders = await this.orderService.AllAsync();

                MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(OrdersCacheDurationMinutes));

                this.memoryCache.Set(OrdersCacheKey, allOrders, cacheEntryOptions);
            }

            return this.View(allOrders);
        }
    }
}
