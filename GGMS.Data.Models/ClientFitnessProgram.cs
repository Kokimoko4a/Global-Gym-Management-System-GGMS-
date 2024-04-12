namespace GGMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ClientFitnessProgram
    {
        [Required]
        public string IdOfClient { get; set; } = null!;

        [Required]
        public ApplicationUser Client { get; set; } = null!;


        [Required]
        public string IdOfFitnessProgram { get; set; } = null!;

        [Required]
        public FitnessProgram FitnessProgram { get; set; } = null!;
    }
}
