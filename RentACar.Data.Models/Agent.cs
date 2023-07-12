namespace RentACar.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Agent;

    public class Agent
    {
        public Agent()
        {
            this.Id = Guid.NewGuid();
            this.OwnedCars = new HashSet<Car>();    
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Car> OwnedCars { get; set; }
    }
}
