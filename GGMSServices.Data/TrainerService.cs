using GGMS.Data;
using GGMS.Data.Models;
using GGMS.Web.ViewModels.Trainer;
using GGMSServices.Data.Interfaces;

namespace GGMSServices.Data
{
    public class TrainerService : ITrainerService
    {
        private readonly ApplicationDbContext data;

       public TrainerService(ApplicationDbContext data) 
        {
                this.data = data;
        }

        public async Task<bool> BecomeTrainer(TainerViewModel trainerViewModel, Guid userId)
        {
            Trainer trainer = new Trainer();

            trainer.Biography = trainerViewModel.Biography;

            trainer.Id = userId;

            data.Add(trainer);

            await data.SaveChangesAsync();

            return true;
        }
    }
}
