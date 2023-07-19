﻿namespace RentACar.Services.Data.Interfaces
{
    using RentACar.Services.Data.Models.Car;
    using RentACar.Web.ViewModels.Car;

    public interface ICarService
    {
        Task CreateAsync(CarFormModel formModel, string agentId);

        Task<AllCarsFilteredAndPagedServiceModel> AllAsync(AllCarsQueryModel queryModel);

        Task<CarDetailsViewModel> GetDetailsByIdAsync(int carId);

        Task<bool> ExistsByIdAsync(int carId);

        Task<CarFormModel> GetCarForEditByIdAsync(int carId);

        Task EditCarByIdAndFormModel(int carId, CarFormModel formModel);
    }
}
