namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.FitnessProgram;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> CheckProgramOwnerAsync(Guid userId, Guid fitnessProgramId)
        {
            var record = await data.UserFitnessPrograms.FirstOrDefaultAsync(x => x.UserId == userId && x.FitnessProgramId == fitnessProgramId);

            return record != null ? true : false;
        }

        public IEnumerable<FitnessProgramSmallViewModel> GetAllAssignedProgramsForUser(Guid id)
        {
            List<Guid> userFitnessPrograms = data.UserFitnessPrograms.Where(x => x.UserId == id)
                 .Select(x => x.FitnessProgramId)
                 .ToList();

            return data.FitnessPrograms.Where(x => userFitnessPrograms.Contains(x.Id)).Select(x => new FitnessProgramSmallViewModel()
            {
                IssueDate = x.IssueDate,
                Id = x.Id,
                Title = x.Title,
            });
        }

        public async Task<ApplicationUser> GetApplicationUserAsync(Guid id)
        {
            return await data.Users.Include(x => x.FitnessCards).Include(x => x.FitnessPrograms).Include(x => x.Trainer).FirstAsync(x => x.Id == id);
        }

        public async Task<FitnessProgramBIgViewModel> GetProgramBigViewForUserAsync(Guid fitnessProgramId)
        {
            var fitnessProgramBaseModel = await data.FitnessPrograms.FirstOrDefaultAsync(x => x.Id == fitnessProgramId);

            return new FitnessProgramBIgViewModel()
            {
                Description = fitnessProgramBaseModel!.Description,
                IssueDate = fitnessProgramBaseModel.IssueDate,
                Id = fitnessProgramId,
                Title = fitnessProgramBaseModel.Title,
            };
        }

        public async Task SaveImagePathAsync(string imagePath, Guid id)
        {
            ApplicationUser applicationUser = await data.Users.FirstAsync(x => x.Id == id);

            applicationUser.PathToImage = imagePath;

            await data.SaveChangesAsync();
        }
    }
}
