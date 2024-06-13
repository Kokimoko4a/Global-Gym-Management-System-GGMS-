


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



        public ICollection<FitnessCard> FitnessCards { get; set; }
    }

}
