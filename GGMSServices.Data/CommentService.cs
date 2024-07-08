namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext data;

        public CommentService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task CreteComment(Guid userId, Guid trainerId, string text)
        {
            var user = await data.Users.FirstOrDefaultAsync(x => x.Id == userId);

            var trainer = await data.Trainers.FirstOrDefaultAsync(x => x.Id == trainerId);

            CommentTrainer commentTrainer = new CommentTrainer();

            commentTrainer.Text = text;
            commentTrainer.IdOfTrainer = trainerId;
            commentTrainer.Trainer = trainer!;
            commentTrainer.Author = user;
            commentTrainer.AuthorId = userId;


            data.Add(commentTrainer);

           

            trainer!.Comments.Add(commentTrainer);

         

            await data.SaveChangesAsync();
        }

        public ICollection<CommentTrainer> GetCommentsForTrainer(Guid trainerId)
        {
            return  data.CommentTrainers.Where(x => x.IdOfTrainer == trainerId).Include(x => x.Author).ToHashSet();
        }

        public async Task<Guid> GetTrainerByComment(Guid commentId)
        {
            var id = await data.CommentTrainers.FirstOrDefaultAsync(x => x.Id == commentId);

            return id.IdOfTrainer;
        }

        public async Task<int> LikeComment(Guid userId, Guid commentId)
        {
            var user = await data.Users.FirstOrDefaultAsync(x => x.Id == userId);

            CommentTrainer commentTrainer = await data.CommentTrainers.FirstOrDefaultAsync(c => c.Id == commentId)!;

            var likeExists = await data.LikeCommentTrainers.FirstOrDefaultAsync(x => x.UserId == userId && x.CommentId == commentId);

            if (likeExists != null)
            {
                return 0;
            }

            LikeCommentTrainer likeCommentTrainer = new LikeCommentTrainer();

            likeCommentTrainer.UserId = userId;
            likeCommentTrainer.User = user!;
            likeCommentTrainer.CommentId = commentId;
            likeCommentTrainer.Comment = commentTrainer!;
            

            data.Add(likeCommentTrainer);

            commentTrainer!.Likes.Add(likeCommentTrainer);

            await data.SaveChangesAsync();

            return commentTrainer.Likes.Count;  
            
        }
    }
}
