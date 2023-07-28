namespace RentACar.Services.Data.Interfaces
{
    using RentACar.Web.ViewModels.Location;
    using Web.ViewModels.Home;

    public interface ILocationService
    {
        Task<IEnumerable<IndexViewModel>> AllAddressesAsync();

        Task<IEnumerable<OrderSelectLocationFormModel>> AllAddressesForOrderAsync();

        Task<bool> ExistsByIdAsync(int id);

        Task<OrderSelectLocationFormModel> GetAddressById(int addressId);

    }
}
