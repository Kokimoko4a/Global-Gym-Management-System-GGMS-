namespace GGMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static GGMS.Common.EntityValidationConstants.FittnessProgramConstants;

    public class FittnessProgram
    {

        public FittnessProgram(string nameOfTrainer, string nameOfClient, ,string description, Trainer trainer, ApplicationUser client)
        {
            Id = Guid.NewGuid().ToString();
            DateAssigned = DateTime.Now;
            NameOfTrainer = nameOfTrainer;
            NameOfClient = nameOfClient;
            Description = description;
            Trainer = trainer;
            Client = client;

            
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

        [Required]
        public ApplicationUser Client { get; set; } = null!;


    }
}
