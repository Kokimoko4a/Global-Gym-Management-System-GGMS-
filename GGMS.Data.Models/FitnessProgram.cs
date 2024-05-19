using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GGMS.Data.Models
{
    public class FitnessProgram
    {
        public FitnessProgram()
        {
            Id = Guid.NewGuid();
            IssueDate = DateTime.Now;
            FitnessProgramUsers = new HashSet<UserFitnessProgram>();
        }

        [Required]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        [ForeignKey(nameof(Trainer))]
        public Guid IdOfTrainer { get; set; } 

        [Required]
        public Trainer Trainer { get; set; } = null!;

        [Required]
        public Gym Gym { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Gym))]
        public Guid GymId { get; set; } 

        [Required]
        public virtual ICollection<UserFitnessProgram> FitnessProgramUsers { get; set; } = null!;



    }
}
