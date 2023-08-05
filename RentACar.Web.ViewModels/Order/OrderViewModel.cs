namespace RentACar.Web.ViewModels.Order
{

    public class OrderViewModel
    {
        public string OrderId { get; set; } = null!;

        public string CarModel { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string RenterEmail { get; set; } = null!;

        public bool IsRented { get; set; }
    }
}
