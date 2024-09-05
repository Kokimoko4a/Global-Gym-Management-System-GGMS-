namespace GGMS.Web.ViewModels.GymOwner
{
    using Microsoft.AspNetCore.Http;
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

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string  Description { get; set; }

        public List<IFormFile> Photos { get; set; } = new List<IFormFile>();

    }
}
