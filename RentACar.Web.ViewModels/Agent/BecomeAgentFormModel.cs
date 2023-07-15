namespace RentACar.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Agent;

    public class BecomeAgentFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

    }
}
