namespace RentACar.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Review;

    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        public virtual Car Car { get; set; } = null!;

        public Guid ReviewerId { get; set; }

        public virtual ApplicationUser Reviewer { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = null!;
    }
}
