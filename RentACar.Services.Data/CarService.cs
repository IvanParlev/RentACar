namespace RentACar.Services.Data
{
    using RentACar.Data.Models;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.Data;
    using RentACar.Web.ViewModels.Car;
    using System;
    using System.Threading.Tasks;

    public class CarService : ICarService
    {
        private readonly RentACarDbContext dbContext;

        public CarService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CarFormModel formModel, string agentId)
        {
            Car newCar = new Car
            {
                Model = formModel.Model,
                Year = formModel.Year,
                NumberOfSeats = formModel.NumberOfSeats,
                GearboxType = formModel.GearboxType,
                FuelType = formModel.FuelType,
                Description = formModel.Description,
                PricePerDay = formModel.PricePerDay,
                ImageUrl = formModel.ImageUrl,
                CategoryId = formModel.CategoryId,
                AgentId = Guid.Parse(agentId)
            };

            await this.dbContext.Cars.AddAsync(newCar);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
