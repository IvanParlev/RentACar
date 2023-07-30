namespace RentACar.Web.ViewModels.Order
{
    using RentACar.Data.Models;
    using RentACar.Web.ViewModels.Location;
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Order;

    public class OrderFormModel
    {
        public OrderFormModel()
        {
            this.Locations = new HashSet<OrderSelectLocationFormModel>();
        }

        public decimal Price { get; set; }

        [Range(DaysRentedMinValue, DaysRentedMaxValue)]
        [Display(Name = "Days Rented")]
        public int DaysRented { get; set; }

        [Display(Name = "Pick Up")]
        public int PickUpLocationId { get; set; }

        [Display(Name = "Drop Off")]
        public int ReturnLocationId { get; set; }

        public IEnumerable<OrderSelectLocationFormModel> Locations { get; set; }

		public bool IsFinalized { get; set; }
	}
}
