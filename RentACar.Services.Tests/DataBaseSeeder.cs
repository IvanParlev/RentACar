namespace RentACar.Services.Tests
{
    using RentACar.Data;
    using RentACar.Data.Models;

    public static class DatabaseSeeder
    {
        public static ApplicationUser AgentUser;
        public static ApplicationUser RenterUser;
        public static Agent Agent;
        public static Location Location;
        public static Category Category;
        public static Review Review;
        public static Order Order;

        public static void SeedDatabase(RentACarDbContext dbContext)
        {
            AgentUser = new ApplicationUser()
            {
                UserName = "ivanAgent@agents.cmo",
                NormalizedUserName = "IVANAGENT@AGENTS.CMO",
                Email = "ivanAgent@agents.cmo",
                NormalizedEmail = "IVANAGENT@AGENTS.CMO",
                EmailConfirmed = false,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "8712100b-7312-4c6f-b443-73056378e4bd",
                SecurityStamp = "44860b20-fb87-4eb9-973c-4f6ffd634465",
                TwoFactorEnabled = false,
            };

            RenterUser = new ApplicationUser()
            {
                UserName = "martinRenter@renter.com",
                NormalizedUserName = "MARTINRENTER@RENTER.COM",
                Email = "martinRenter@renter.com",
                NormalizedEmail = "MARTINRENTER@RENTER.COM",
                EmailConfirmed = false,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                ConcurrencyStamp = "630b5c16-5e51-4152-85ec-3f01346042fe",
                SecurityStamp = "44860b20-fb87-4eb9-973c-4f6ffd634465",
                TwoFactorEnabled = false,
            };

            Agent = new Agent()
            {
                Name = "Agent",
                User = AgentUser
            };
         
            Location = new Location()
            {
                Id = 15,
                Address = "Sofia"
            };

            Category = new Category()
            {
                Id = 5,
                Name = "Cabriolet"
            };

            Review = new Review()
            {
                CarId = 1,
                Rating = 5,
                Comment = "Very Nice Car"
            };

            //Order = new Order()
            //{
            //    CarId = 2,
            //    DaysRented = 2,
            //    Price = 200,
            //    PickUpLocationId = 6,
            //    ReturnLocationId = 3,
            //    IsFinalized = true,
            //    RenterId = Guid.Parse("88899F53-2C6A-4624-17E3-08DB822F58CA") 
            //};
            //dbContext.Orders.Add(Order);
            Order = new Order()
            {
                CarId = 1,
                DaysRented = 2,
                Price = 160,
                PickUpLocationId = 2,
                ReturnLocationId = 6,
                IsFinalized = true,
                RenterId = RenterUser.Id,
            };
            dbContext.Users.Add(AgentUser);
            dbContext.Users.Add(RenterUser);
            dbContext.Agents.Add(Agent);
            dbContext.Locations.Add(Location);
            dbContext.Categories.Add(Category);
            dbContext.Reviews.Add(Review);
            dbContext.Orders.Add(Order);


            dbContext.SaveChanges();
        }

    }
}
