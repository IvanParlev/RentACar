namespace RentACar.Web.ViewModels.Review
{
    public class ReviewDetailsViewModel : ReviewFormModel
    {
        public Guid ReviewerId { get; set; }

        public int CarId { get; set; }
    }
}
