namespace GGMS.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.ValidationConstants.TrainerValidationConstants;

    public class Trainer
    {
        public Trainer()
        {
            FitnessPrograms = new HashSet<FitnessProgram>();
            Clients = new HashSet<ApplicationUser>();
            Requests = new HashSet<RequestToTrainer>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        public ICollection<FitnessProgram> FitnessPrograms { get; set; } = null!;

        public ICollection<ApplicationUser> Clients { get; set; } = null!;

        [Required]
        [StringLength(BioghraphyMaxLength , MinimumLength = BioghraphyMinLength)]
        public string Biography { get; set; } = null!;

        public ICollection<RequestToTrainer> Requests { get; set; }
    }
}
