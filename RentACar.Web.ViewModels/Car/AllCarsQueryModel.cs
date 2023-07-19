namespace RentACar.Web.ViewModels.Car
{
    using System.ComponentModel.DataAnnotations;

    using RentACar.Web.ViewModels.Car.Enums;

    using static Common.ApplicationConstants;

    public class AllCarsQueryModel
    {
        public AllCarsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.CarsPerPage = EntitiesPerPage;

            this.Categories = new HashSet<string>();
            this.Cars = new HashSet<CarAllViewModel>();
        }

        public string? Category { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort Cars By")]
        public CarSorting CarSorting { get; set; }

        public int CurrentPage { get; set; }

		[Display(Name = "Show cars on page")]
        public int CarsPerPage { get; set; }

        public int TotalCars { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<CarAllViewModel> Cars { get; set; }
    }
}
