namespace GGMS.Web.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.UserValidationConstants;

    public class LoginFormModel
    {
        [Required]
        [StringLength(MaxEmailLength, MinimumLength = MinEmailLength)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(MaxPasswordLength, MinimumLength = MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}