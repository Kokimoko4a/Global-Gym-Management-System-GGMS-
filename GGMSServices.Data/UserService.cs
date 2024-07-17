namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<ApplicationUser> GetApplicationUserAsync(Guid id)
        {
            return await data.Users.Include(x => x.FitnessCards).Include(x => x.FitnessPrograms).Include(x => x.Trainer).FirstAsync(x => x.Id == id);
        }

        public async Task SaveImagePathAsync(string imagePath, Guid id)
        {
            ApplicationUser applicationUser = await data.Users.FirstAsync(x => x.Id == id);

            applicationUser.PathToImage = imagePath;

            await data.SaveChangesAsync();
        }
    }
}
