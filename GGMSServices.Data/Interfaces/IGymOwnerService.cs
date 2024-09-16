namespace GGMSServices.Data.Interfaces
{
    using GGMS.Web.ViewModels.GymOwner;

    public interface IGymOwnerService
    {
        public Task<bool> IsGymOwner(Guid id);

        public Task BecomeGymOwner(Guid id);

        public Task CreateGym(GymFormModel formModel, Guid creatorId);

        public IEnumerable<GymSmallViewModel> GetAllGyms();

        public GymBigViewModel GetGymAsBigViewModel(Guid id);
            
    }
}
