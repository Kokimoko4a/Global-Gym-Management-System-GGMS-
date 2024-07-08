using GGMS.Data.Models;

namespace GGMSServices.Data.Interfaces
{
    public interface ICommentService
    {
        public Task CreteComment(Guid userId, Guid trainerId, string text);

        public ICollection<CommentTrainer> GetCommentsForTrainer(Guid trainerId);

        public Task<int> LikeComment(Guid userId, Guid commentId);

        public Task<Guid> GetTrainerByComment(Guid commentId);
    }
}
