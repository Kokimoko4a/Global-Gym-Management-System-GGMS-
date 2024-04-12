namespace GGMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static GGMS.Common.EntityValidationConstants.FittnessProgramConstants;

    public class FitnessProgram
    {

        public FitnessProgram()
        {
            Id = Guid.NewGuid().ToString();
            DateAssigned = DateTime.Now;
            Clients = new HashSet<ClientFitnessProgram>();

            
        }

        [Key]   
        public string Id { get; set; }

        [Required]
        [StringLength(MaxLengthOfNameTrainer, MinimumLength = MinLengthOfNameTrainer)]
        public string NameOfTrainer { get; set; } = null!;

        [Required]
        [StringLength(MaxLengthOfNameClient, MinimumLength = MinLengthOfNameclient)]
        public string NameOfClient { get; set; } = null!;

        [Required]
        [StringLength(MaxLengthOfDescription, MinimumLength = MinLengthOfDescription)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DateAssigned { get; set; }

        [Required]
        public Trainer Trainer { get; set; } = null!;

        [ForeignKey(nameof(Trainer))]
        [Required]
        public string TrainerId { get; set; } = null!;

        [Required]
        public virtual ICollection<ClientFitnessProgram> Clients { get; set; }


    }
}
