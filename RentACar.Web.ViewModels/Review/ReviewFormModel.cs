namespace RentACar.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Review;

    public class ReviewFormModel
    {
        [Range(ReviewMinRatingValue, ReviewMaxRatingValue)]
        public int Rating { get; set; }

        [Required]
        [StringLength(CommentMaxLength, MinimumLength = CommentMinLength)]
        public string Comment { get; set; } = null!;
    }
}
