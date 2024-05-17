using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GGMS.Data.Models
{
    public class FitnessProgram
    {
        public FitnessProgram()
        {
            Id = Guid.NewGuid().ToString();
            IssueDate = DateTime.Now;
           // FitnessProgramUsers = new HashSet<string>();
        }

        [Required]
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime IssueDate { get; set; }

     /*   [Required]
        [ForeignKey(nameof(Trainer))]
        public string IdOfTrainer { get; set; } = null!;

        [Required]
        public string Trainer { get; set; } = null!;

        [Required]
        public virtual ICollection<string> FitnessProgramUsers { get; set; } = null!;*/



    }
}
