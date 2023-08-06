namespace RentACar.Services.Data
{
    using System;
    using System.Threading.Tasks;
    
    using Microsoft.EntityFrameworkCore;

    using RentACar.Data.Models;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Services.Data.Models.Car;
    using RentACar.Web.ViewModels.Car;
    using RentACar.Web.ViewModels.Car.Enums;
    using RentACar.Data;

    public class CarService : ICarService
    {
        private readonly RentACarDbContext dbContext;

        public CarService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AllCarsFilteredAndPagedServiceModel> AllAsync(AllCarsQueryModel queryModel)
        {
            IQueryable<Car> carsQuery = this.dbContext
                .Cars
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                carsQuery = carsQuery
                    .Where(c => c.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";
                carsQuery = carsQuery
                    .Where(c => EF.Functions.Like(c.Model, wildCard) ||
                                EF.Functions.Like(c.Description, wildCard));
            }

            carsQuery = queryModel.CarSorting switch
            {
                CarSorting.PriceAscending => carsQuery
                .OrderBy(c => c.PricePerDay),
                CarSorting.PriceDescending => carsQuery
                .OrderByDescending(c => c.PricePerDay),
                _ => carsQuery
                .OrderBy(c => c.RenterId != null)
                .ThenBy(c => c.Model)
            };
            IEnumerable<CarAllViewModel> allCars = await carsQuery
               .Where(c => c.IsActive)
               .Skip((queryModel.CurrentPage - 1) * queryModel.CarsPerPage)
               .Take(queryModel.CarsPerPage)
               .Select(c => new CarAllViewModel()
               {
                   Id = c.Id,
                   CarModel = c.Model,
                   Year = c.Year,
                   PricePerDay = c.PricePerDay,
                   ImageUrl = c.ImageUrl,
                   IsRented = c.RenterId.HasValue
               })
               .ToArrayAsync();
            int totalCars = carsQuery.Count();

            return new AllCarsFilteredAndPagedServiceModel()
            {
                TotalCarsCount = totalCars,
                Cars = allCars
            };
        }

        public async Task<int> CreateAndReturnIdAsync(CarFormModel formModel, string agentId)
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

            return newCar.Id;
        }

        public async Task DeleteCarByIdAsync(int carId)
        {
            Car carToDelete = await this.dbContext
                .Cars
                .Where(c => c.IsActive)
                .FirstAsync(c => c.Id == carId);

            carToDelete.IsActive = false;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditCarByIdAndFormModelAsync(int carId, CarFormModel formModel)
        {
            Car car = await this.dbContext
                .Cars
                .Where(c => c.IsActive)
                .FirstAsync(c => c.Id == carId);

            car.Model = formModel.Model;
            car.Year = formModel.Year;
            car.NumberOfSeats = formModel.NumberOfSeats;
            car.GearboxType = formModel.GearboxType;
            car.FuelType = formModel.FuelType;
            car.Description = formModel.Description;
            car.ImageUrl = formModel.ImageUrl;
            car.PricePerDay = formModel.PricePerDay;
            car.CategoryId = formModel.CategoryId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int carId)
        {
            bool result = await this.dbContext
               .Cars
               .Where(c => c.IsActive)
               .AnyAsync(c => c.Id == carId);

            return result;
        }

        public async Task<CarDeleteDetailsViewModel> GetCarForDeleteByIdAsync(int carId)
        {
            Car car = await this.dbContext
                .Cars
                .Where(c => c.IsActive)
                .FirstAsync(c => c.Id == carId);

            return new CarDeleteDetailsViewModel
            {
                CarModel = car.Model,
                ImageUrl = car.ImageUrl
            };
        }

        public async Task<CarFormModel> GetCarForEditByIdAsync(int carId)
        {
            Car car = await this.dbContext
               .Cars
               .Include(c => c.Category)
               .Where(c => c.IsActive)
               .FirstAsync(c => c.Id == carId);

            return new CarFormModel
            {
                Model = car.Model,
                Year = car.Year,
                Description = car.Description,
                ImageUrl = car.ImageUrl,
                PricePerDay = car.PricePerDay,
                CategoryId = car.CategoryId,
                FuelType = car.FuelType,
                GearboxType = car.GearboxType,
                NumberOfSeats = car.NumberOfSeats
            };
        }

        public async Task<CarDetailsViewModel> GetDetailsByIdAsync(int carId)
        {
            Car car = await this.dbContext
                .Cars
                .Include(c => c.Category)
                .Where(c => c.IsActive)
                .FirstAsync(c => c.Id == carId);

            return new CarDetailsViewModel
            {
                Id = car.Id,
                CarModel = car.Model,
                Year = car.Year,
                Category = car.Category.Name,
                ImageUrl = car.ImageUrl,
                PricePerDay = car.PricePerDay,
                GearboxType = car.GearboxType,
                FuelType = car.FuelType,
                Description = car.Description,
                NumberOfSeats = car.NumberOfSeats,
                RenterId = car.RenterId,
                IsRented = car.RenterId.HasValue
            };
        }

        public async Task<bool> IsRentedAsync(int carId)
        {
            Car car = await this.dbContext
                .Cars
                .FirstAsync(c => c.Id == carId);

            return car.RenterId.HasValue;
        }

        public async Task RentCarAsync(string orderId)
        {
            Order order = await this.dbContext
                .Orders
                .FirstAsync(o => o.Id.ToString() == orderId);

            Car car = await this.dbContext
                .Cars
                .Where(c => c.IsActive)
                .FirstAsync(c => c.Id == order.CarId);
            car.RenterId = Guid.Parse(order.RenterId.ToString());

            await this.dbContext.SaveChangesAsync();
        }
    }
}
