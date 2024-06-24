namespace GGMS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static GGMS.Common.ValidationConstants.CommentTrainer;

    public class CommentTrainer
    {
        public CommentTrainer()
        {
            Id = Guid.NewGuid();
            Likes = new HashSet<LikeCommentTrainer>();
            DisLikes = new HashSet<DislikeCommentTrainer>();
            CreatedOn = DateTime.Now;
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public ApplicationUser Author { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Trainer))]
        public Guid IdOfTrainer { get; set; }

        public Trainer Trainer { get; set; } = null!;

        [Required]
        [StringLength(TextMaxLength,MinimumLength = TextMinLength)]
        public string Text { get; set; } = null!;

        public ICollection<LikeCommentTrainer> Likes { get; set; } = null!;

        public ICollection<DislikeCommentTrainer> DisLikes { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

    }
}
