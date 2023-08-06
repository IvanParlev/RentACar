namespace RentACar.Web.ViewModels.Car
{
    public class CarDetailsViewModel : CarAllViewModel
    {
        public Guid? RenterId { get; set; }

        public string? Description { get; set; }

		public int NumberOfSeats { get; set; }

        public string Category { get; set; } = null!;
	}
}
