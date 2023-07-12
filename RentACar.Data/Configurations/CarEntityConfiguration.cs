namespace RentACar.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using RentACar.Data.Models;

    public class CarEntityConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder
                .HasOne(c => c.Category)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Agent)
                .WithMany(c => c.OwnedCars)
                .HasForeignKey(c => c.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(this.GenerateCars());
        }

        private Car[] GenerateCars()
        {
            ICollection<Car> cars = new HashSet<Car>();

            Car car;

            car = new Car()
            {
                Id = 1,
                Model = "VW Golf 8",
                CategoryId = 4,
                Year = 2022,
                FuelType = Models.Enums.FuelType.Electric,
                GearboxType = Models.Enums.GearboxType.Automatic,
                Description = "Volkswagen Golf 8 is the beginning of a whole new generation of models. With many digital innovations, dynamic design and exceptional comfort of function management. The new Golf is a great choice for a compact rental car.",
                NumberOfSeats = 5,
                PricePerDay = 90,
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8a/2020_Volkswagen_Golf_Style_1.5_Front.jpg",
                AgentId = Guid.Parse("3F7BC99C-804B-4975-9F2E-C2F6A0AE3B5E")
            };
            cars.Add(car);

            car = new Car()
            {
                Id = 2,
                Model = "Toyota Corolla",
                CategoryId = 1,
                Year = 2021,
                FuelType = Models.Enums.FuelType.Petrol,
                GearboxType = Models.Enums.GearboxType.Manual,
                Description = "Toyota Corolla offers everything that might be expected from Toyota: it has a stylish look, sporty handling, fuel efficiency. It is great car rental choice.",
                NumberOfSeats = 5,
                PricePerDay = 100,
                ImageUrl = "https://di-uploads-pod28.dealerinspire.com/colonialtoyota/uploads/2020/09/2021-Toyota-Corolla-Indiana-PA-White-Left-1.jpg",
                AgentId = Guid.Parse("3F7BC99C-804B-4975-9F2E-C2F6A0AE3B5E")
            };
            cars.Add(car);

            car = new Car()
            {
                Id = 3,
                Model = "VW Passat",
                CategoryId = 2,
                Year = 2022,
                FuelType = Models.Enums.FuelType.Diesel,
                GearboxType = Models.Enums.GearboxType.Automatic,
                Description = "The new VW Passat station wagon with speed automatic transmission has notable vision and very clean shapes. You can use it to travel with your family or friends - a great choice for car rental.",
                NumberOfSeats = 5,
                PricePerDay = 110,
                ImageUrl = "https://resource.digitaldealer.com.au/image/15009168736417dea75b995464285423_900_600-c.jpg",
                AgentId = Guid.Parse("3F7BC99C-804B-4975-9F2E-C2F6A0AE3B5E")
            };
            cars.Add(car);

            car = new Car()
            {
                Id = 4,
                Model = "Hyundai Tucson",
                CategoryId = 3,
                Year = 2022,
                FuelType = Models.Enums.FuelType.Petrol,
                GearboxType = Models.Enums.GearboxType.Automatic,
                Description = "If the pleasure of driving in natural landscapes is what you are looking for, then Hyundai Tucson 4x4 is your car. Hyundai's new crossover for rent is designed to excel in all areas.",
                NumberOfSeats = 5,
                PricePerDay = 120,
                ImageUrl = "https://media.ed.edmunds-media.com/hyundai/tucson/2022/oem/2022_hyundai_tucson_4dr-suv_limited_fq_oem_1_1280.jpg",
                AgentId = Guid.Parse("3F7BC99C-804B-4975-9F2E-C2F6A0AE3B5E")
            };
            cars.Add(car);

            return cars.ToArray();
        }

    }
}
