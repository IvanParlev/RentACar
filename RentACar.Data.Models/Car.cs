namespace RentACar.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using RentACar.Data.Models.Enums;

    using static Common.EntityValidationConstants.Car;

    public class Car
    {
        public Car()
        {
            this.Reviews = new HashSet<Review>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Required] 
        public FuelType FuelType { get; set; }

        [Required] 
        public GearboxType GearboxType { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

		public bool IsActive { get; set; }

		public Guid AgentId { get; set; }

        public virtual Agent Agent { get; set; } = null!;

        public Guid? RenterId { get; set; }

        public virtual ApplicationUser? Renter { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; } 
    }
}
