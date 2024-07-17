namespace GGMSServices.Data.Interfaces
{
    using GGMS.Data.Models;

    public interface IUserService
    {
        public Task<ApplicationUser> GetApplicationUserAsync(Guid id);

        public Task SaveImagePathAsync(string imagePath, Guid id);
    }
}
