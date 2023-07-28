namespace RentACar.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Home;
    using RentACar.Web.ViewModels.Location;
    using RentACar.Data.Models;
    using RentACar.Data;

    public class LocationService : ILocationService
    {
        private readonly RentACarDbContext dbContext;

        public LocationService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllAddressesAsync()
        {
            IEnumerable<IndexViewModel> allAddresses = await dbContext
                 .Locations
                 .AsNoTracking()
                 .OrderBy(l => l.Id)
                 .Select(l => new IndexViewModel()
                 {
                     Id = l.Id,
                     Address = l.Address
                 })
                 .ToArrayAsync();

            return allAddresses;
        }

        public async Task<IEnumerable<OrderSelectLocationFormModel>> AllAddressesForOrderAsync()
        {
            IEnumerable<OrderSelectLocationFormModel> allAddresses = await dbContext
                 .Locations
                 .AsNoTracking()
                 .OrderBy(l => l.Id)
                 .Select(l => new OrderSelectLocationFormModel()
                 {
                     Id = l.Id,
                     Address = l.Address
                 })
                 .ToArrayAsync();

            return allAddresses;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await dbContext
                 .Locations
                 .AnyAsync(l => l.Id == id);

            return result;
        }

        public async Task<OrderSelectLocationFormModel> GetAddressById(int addressId)
        {
            Location location = await dbContext
                 .Locations
                 .FirstAsync(l => l.Id == addressId);

            return new OrderSelectLocationFormModel
            {
                Address = location.Address,
            };
        }
    }
}
