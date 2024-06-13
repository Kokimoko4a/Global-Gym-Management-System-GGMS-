

namespace GGMS.Web.ViewModels.FitnessProgram
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.FitnessProgramValidationConstants;
    using GGMS.Data.Models;
   

    public class FitnessProgramFormModel
    {
        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime IssueDate { get; set; }

        

        public Guid IdOfTrainer { get; set; }




   

   

    }
}
