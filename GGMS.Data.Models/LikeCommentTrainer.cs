namespace GGMS.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LikeCommentTrainer
    {
        public LikeCommentTrainer()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Comment))]
        public Guid CommentId { get; set; }

        public CommentTrainer Comment { get; set; } = null!;
    }
}
