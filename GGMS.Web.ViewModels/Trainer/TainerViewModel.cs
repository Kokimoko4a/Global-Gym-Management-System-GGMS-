
namespace GGMS.Web.ViewModels.Trainer
{
    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.TrainerValidationConstants;
    public class TainerViewModel
    {
        [Required]
        [StringLength(BioghraphyMaxLength, MinimumLength = BioghraphyMinLength)]
        public string Biography { get; set; } = null!;
    }
}
