using GGMS.Web.ViewModels.Trainer;

namespace GGMSServices.Data.Interfaces
{
    public interface ITrainerService
    {
        public Task<bool> BecomeTrainer(TainerViewModel tainerViewModel, Guid Id);
    }
}
