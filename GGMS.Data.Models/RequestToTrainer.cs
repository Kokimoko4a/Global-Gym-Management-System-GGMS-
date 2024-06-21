
namespace GGMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static GGMS.Common.ValidationConstants.RequestToTrainer;

    public class RequestToTrainer
    {
        public RequestToTrainer()
        {
            Id = Guid.NewGuid();   
        }

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Trainer))]
        public Guid IdOfTrainer { get; set; }

        public Trainer Trainer { get; set; } = null!;

        [ForeignKey(nameof(Client))]
        public Guid IdOfClient { get; set; }

        public ApplicationUser Client { get; set; } = null!;

        public bool IsApproved { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength)]
        public string DecriptionOfRequest { get; set; }
    }
}
