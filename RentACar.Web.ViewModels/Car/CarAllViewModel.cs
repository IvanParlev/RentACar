namespace RentACar.Web.ViewModels.Car
{
    using System.ComponentModel.DataAnnotations;

    using RentACar.Data.Models.Enums;

    public class CarAllViewModel
    {
        public int Id { get; set; }

        public string CarModel { get; set; } = null!;

        public int Year { get; set; }

        [Display(Name = "Gearbox Type")]
        public GearboxType GearboxType { get; set; }

        [Display(Name = "Fuel Type")]
        public FuelType FuelType { get; set; }

        public decimal PricePerDay { get; set; }

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }

    }
}
