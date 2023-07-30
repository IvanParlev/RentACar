namespace RentACar.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using RentACar.Data;
    using RentACar.Data.Models;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Car;
    using RentACar.Web.ViewModels.Order;

    public class OrderService : IOrderService
    {
        private readonly RentACarDbContext dbContext;

        public OrderService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDetailsViewModel>> AllByUserIdAsync(string userId)
        {          
            IEnumerable<OrderDetailsViewModel> allUsersOrders = await this.dbContext
                 .Orders
                 .Where(o => o.IsFinalized &&
                             o.RenterId.ToString() == userId)
                 .Select(o => new OrderDetailsViewModel()
                 {
                     Id = o.Id,
                     CarId = o.CarId,
                     DaysRented = o.DaysRented,
                     Price = o.Price,
                     PickUpLocationId = o.PickUpLocationId,
                     ReturnLocationId = o.ReturnLocationId,
                 })
                 .ToArrayAsync();

            return allUsersOrders;
        }

        public async Task CreateOrderAsync(int carId, OrderFormModel formModel, string renterId)
        {
            Car car = await this.dbContext
                .Cars
                .Where(c => c.IsActive)
                .FirstAsync(c => c.Id == carId);


            Order newOrder = new Order()
            {
                CarId = carId,
                RenterId = Guid.Parse(renterId),
                DaysRented = formModel.DaysRented,
                Price = car.PricePerDay * formModel.DaysRented,
                PickUpLocationId = formModel.PickUpLocationId,
                ReturnLocationId = formModel.ReturnLocationId,
                IsFinalized = true
            };


            await this.dbContext.Orders.AddAsync(newOrder);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            bool result = await this.dbContext
               .Orders
               .Where(o => o.IsFinalized)
               .AnyAsync(o => o.Id.ToString() == id);

            return result;
        }

        public async Task<OrderDetailsViewModel> GetOrderDetailsAsync(string orderId)
        {
            Order order = await this.dbContext
                .Orders
                .FirstAsync(o => o.Id.ToString() == orderId &&
                            o.IsFinalized);

            return new OrderDetailsViewModel
            {
                CarId = order.CarId,
                DaysRented = order.DaysRented,
                Price = order.Price,
                PickUpLocationId = order.PickUpLocationId,
                ReturnLocationId = order.ReturnLocationId,
            };
        }
    }
}
