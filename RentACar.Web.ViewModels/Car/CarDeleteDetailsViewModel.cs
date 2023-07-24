namespace RentACar.Web.ViewModels.Car
{
    using System.ComponentModel.DataAnnotations;

    public class CarDeleteDetailsViewModel
    {
        [Display(Name = "Car Model")]
        public string CarModel { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
