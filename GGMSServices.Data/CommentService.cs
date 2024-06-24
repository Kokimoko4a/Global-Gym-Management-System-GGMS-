using GGMS.Data;
using GGMS.Data.Models;
using GGMSServices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GGMSServices.Data
{
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
    }
}
