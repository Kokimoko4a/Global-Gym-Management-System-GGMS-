using GGMS.Data.Models;

namespace GGMSServices.Data.Interfaces
{
    public interface ICommentService
    {
        public Task CreteComment(Guid userId, Guid trainerId, string text);

        public ICollection<CommentTrainer> GetCommentsForTrainer(Guid trainerId);
    }
}
