using GGMS.Web.ViewModels.GymOwner;

namespace GGMSServices.Data.Interfaces
{
    public interface IGymOwnerService
    {
        public Task<bool> IsGymOwner(Guid id);

        public Task BecomeGymOwner(Guid id);

        public Task CreateGym(GymFormModel formModel, Guid creatorId);
            
    }
}
