namespace RentACar.Web.ViewModels.Order
{
    public class OrderDetailsViewModel : OrderFormModel
    {
        public Guid Id { get; set; }

        public int CarId { get; set; }
    }
}
