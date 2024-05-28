namespace GGMS.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.UserValidationConstants;

    public class RegisterFormModel
    {
        [Required]
        [StringLength(MaxEmailLength, MinimumLength = MinEmailLength)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(MaxPasswordLength, MinimumLength = MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Range(AgeMinValue, AgeMaxValue)]
        public int Age { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(TelephoneNumberMaxLength, MinimumLength = TelephoneNumberMinLength)]
        public string TelephoneNumber { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; } = null!;
    }
}
