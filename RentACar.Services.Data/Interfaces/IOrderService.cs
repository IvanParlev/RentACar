namespace RentACar.Services.Data.Interfaces
{
    using RentACar.Web.ViewModels.Car;
    using RentACar.Web.ViewModels.Order;

    public interface IOrderService
    {
        Task CreateOrderAsync(int carId, OrderFormModel formModel, string renterId);

        Task<IEnumerable<OrderDetailsViewModel>> AllByUserIdAsync(string userId);

        Task<OrderDetailsViewModel> GetOrderDetailsAsync(string orderId);

        Task<bool> ExistsByIdAsync(string id);
    }
}
