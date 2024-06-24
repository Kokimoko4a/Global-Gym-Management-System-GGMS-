namespace GGMS.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static GGMS.Common.ValidationConstants.UserValidationConstants;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
            FitnessPrograms = new HashSet<UserFitnessProgram>();
            FitnessCards = new HashSet<FitnessCard>();
            Requests = new HashSet<RequestToTrainer>();
            Likes = new HashSet<LikeCommentTrainer>();
            DisLikes = new HashSet<DislikeCommentTrainer>();
        }

        public ICollection<UserFitnessProgram> FitnessPrograms { get; set; } = null!;

        public ICollection<FitnessCard> FitnessCards { get; set; } = null!;

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string LastName { get; set; } = null!;

        [Required]
        [Range(AgeMinValue,AgeMaxValue)]
        public int Age { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        public string TelephoneNumber { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

  
        public Trainer Trainer { get; set; } = null!;

        [ForeignKey(nameof(Trainer))]
        
        public Guid? TrainerId { get; set; }

        public ICollection<RequestToTrainer> Requests { get; set; }

        public ICollection<LikeCommentTrainer> Likes { get; set; } = null!;

        public ICollection<DislikeCommentTrainer> DisLikes { get; set; } = null!;
    }
}
