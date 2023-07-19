namespace RentACar.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using RentACar.Data.Models;
    using RentACar.Services.Data.Interfaces;
    using RentACar.Services.Data.Models.Car;
    using RentACar.Web.Data;
    using RentACar.Web.ViewModels.Car;
    using RentACar.Web.ViewModels.Car.Enums;

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
                   GearboxType = c.GearboxType,
                   FuelType = c.FuelType,
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

		public async Task EditCarByIdAndFormModel(int carId, CarFormModel formModel)
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
                IsRented = car.RenterId.HasValue
            };
        }
    }
}
