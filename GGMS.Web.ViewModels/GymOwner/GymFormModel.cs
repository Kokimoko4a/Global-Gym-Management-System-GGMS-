
namespace GGMS.Web.ViewModels.GymOwner
{
    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.GymValidationConstants;

    public class GymFormModel
    {

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

    }
}
