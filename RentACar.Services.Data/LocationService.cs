namespace Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using RentACar.Services.Data.Interfaces;
    using RentACar.Web.ViewModels.Home;
    using RentACar.Web.Data;

    public class LocationService : ILocationService
    {
        private readonly RentACarDbContext dbContext;

        public LocationService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllAddressesAsync()
        {
            IEnumerable<IndexViewModel> allAddresses = await this.dbContext
                 .Locations
                 .OrderBy(l => l.Id)
                 .Select(l => new IndexViewModel()
                 {
                     Id = l.Id,
                     Address = l.Address
                 })
                 .ToArrayAsync();
            return allAddresses;
        }
    }
}
