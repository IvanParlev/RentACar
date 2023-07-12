namespace RentACar.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RentACar.Data.Models;

    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasData(this.GenerateLocations());
        }

        private Location[] GenerateLocations()
        {
            ICollection<Location> locations = new HashSet<Location>();

            Location location;

            location = new Location()
            {
                Id = 1,
                Address = "Sofia - Airport"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 2,
                Address = "Plovdiv - Airport"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 3,
                Address = "Varna - Airport"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 4,
                Address = "Burgas - Airport"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 5,
                Address = "Sofia - office"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 6,
                Address = "Plovdiv - office"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 7,
                Address = "Varna - office"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 8,
                Address = "Burgas - office"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 9,
                Address = "Sozopol - office"
            };
            locations.Add(location);

            location = new Location()
            {
                Id = 10,
                Address = "Sunny Beach - office"
            };
            locations.Add(location);

            return locations.ToArray();
        }
    }
}
