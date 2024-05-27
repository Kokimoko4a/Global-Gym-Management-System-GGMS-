


namespace GGMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static GGMS.Common.ValidationConstants.GymValidationConstants;


    public class Gym
    {
        public Gym()
        {
            Id = Guid.NewGuid();
            FitnessPrograms = new HashSet<FitnessProgram>();
            FitnessCards = new HashSet<FitnessCard>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(NameMaxLength,MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength,MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        public GymOwner Owner { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        [Required]
        public ICollection<FitnessProgram> FitnessPrograms { get; set; }

        public ICollection<FitnessCard> FitnessCards { get; set; }
    }

}
