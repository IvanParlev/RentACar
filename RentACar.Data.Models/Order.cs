namespace RentACar.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Order;

    public class Order
    {
        public Order()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public virtual Car Car { get; set; } = null!;

        public Guid RenterId { get; set; }

        public virtual ApplicationUser Renter { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int DaysRented { get; set; }

        [Required]
        public int PickUpLocationId { get; set; }

        public virtual Location PickUpLocation { get; set; } = null!;

        [Required]
        public int ReturnLocationId { get; set; }

        public virtual Location ReturnLocation { get; set; } = null!;
    }
}
