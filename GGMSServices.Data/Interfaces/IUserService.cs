namespace GGMSServices.Data.Interfaces
{
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.FitnessProgram;

    public interface IUserService
    {
        public Task<ApplicationUser> GetApplicationUserAsync(Guid id);

        public Task SaveImagePathAsync(string imagePath, Guid id);

        public IEnumerable<FitnessProgramSmallViewModel> GetAllAssignedProgramsForUser(Guid id);

        public Task<FitnessProgramBIgViewModel> GetProgramBigViewForUserAsync(Guid fitnessProgramId);

        public Task<bool> CheckProgramOwnerAsync(Guid userId, Guid fitnessProgramId);
    }
}
