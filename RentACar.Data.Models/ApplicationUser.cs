namespace RentACar.Data.Models
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.RentedCars = new HashSet<Car>();
        }
        public virtual ICollection<Car> RentedCars { get; set; }

    }
}
