namespace RentACar.Services.Data.Interfaces
{
    using RentACar.Web.ViewModels.Car;

    public interface ICarService
    {
        Task CreateAsync(CarFormModel formModel, string agentId);

    }
}
