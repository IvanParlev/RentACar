namespace RentACar.Services.Data.Interfaces
{
    using Web.ViewModels.Home;

    public interface ILocationService
    {
        Task<IEnumerable<IndexViewModel>> AllAddressesAsync();

    }
}
