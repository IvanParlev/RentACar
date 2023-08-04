namespace RentACar.Web.ViewModels.Car
{
    using System.ComponentModel.DataAnnotations;

    using RentACar.Data.Models.Enums;
    using RentACar.Web.ViewModels.Category;

    using static Common.EntityValidationConstants.Car;
    using static Common.ApplicationConstants;

    public class CarFormModel
    {
        public CarFormModel()
        {
            this.Categories = new HashSet<CarSelectCategoryFormModel>();
        }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; } = null!;

        [Range(MinYearProduced, ReleaseYear)]
        [Display(Name = "Year of Manufacture")]
        public int Year { get; set; }

        [Range(MinSeatsValue, MaxSeatsValue)]
        [Display(Name = "Number of Seats")]
        public int NumberOfSeats { get; set; }

        [Display(Name = "Gearbox Type")]
        public GearboxType GearboxType { get; set; }

        [Display(Name = "Fuel Type")]
        public FuelType FuelType { get; set; }

        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string? Description { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        [Range(typeof(decimal), PricePerDayMinValue, PricePerDayMaxValue)]
        [Display(Name = "Daily Price")]
        public decimal PricePerDay { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CarSelectCategoryFormModel> Categories { get; set; }
    }
}
