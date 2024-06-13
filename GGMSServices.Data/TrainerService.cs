namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.FitnessProgram;
    using GGMS.Web.ViewModels.Trainer;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class TrainerService : ITrainerService
    {
        private readonly ApplicationDbContext data;

        public TrainerService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> AddProgramAsync(FitnessProgramFormModel model)
        {
            FitnessProgram fitnessProgram = new FitnessProgram();

            fitnessProgram.IdOfTrainer = model.IdOfTrainer;
            fitnessProgram.Description = model.Description;
            fitnessProgram.Title = model.Title;

            await data.AddAsync(fitnessProgram);

            await data.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<FitnessProgramSmallViewModel>> AllProgramsAsync(Guid id)
        {
            List<FitnessProgramSmallViewModel> programs = await data.FitnessPrograms.Where(x => x.IdOfTrainer == id)
                .Select(x => new FitnessProgramSmallViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    IssueDate = x.IssueDate
                }).ToListAsync();

            return programs;


        }

        public async Task<bool> BecomeTrainer(TrainerFormModel trainerViewModel, Guid userId)
        {
            Trainer trainer = new Trainer();

            trainer.Biography = trainerViewModel.Biography;

            trainer.Id = userId;

            data.Add(trainer);

            await data.SaveChangesAsync();

            return true;
        }

        public async Task DeleteProgramAsync(Guid id)
        {
            var program = await data.FitnessPrograms.FirstOrDefaultAsync(x => x.Id == id);

            data.FitnessPrograms.Remove(program!);

            await data.SaveChangesAsync();
        }

        public async Task EditProgramAsync(FitnessProgramFormModel model)
        {
            var program = await data.FitnessPrograms.FirstOrDefaultAsync(x => x.Id == model.Id);

            program.Description = model.Description;
            program.Title = model.Title;

            await data.SaveChangesAsync();
        }

        public AllTrainers GetAllTrainers()
        {
            AllTrainers allTrainers = new AllTrainers();

            allTrainers.Trainers = data.Users.Select(t => new TrainerSmallViewModel()
            {
                Id = t.Id,
                FirstName = t.FirstName,
                LastName = t.LastName
            }).ToHashSet();


            return allTrainers;
        }

        public async Task<FitnessProgramFormModel> GetProgramAsFormModel(Guid id)
        {
            var program = await data.FitnessPrograms.FirstOrDefaultAsync(x => x.Id == id);

            FitnessProgramFormModel formModel = new FitnessProgramFormModel();

            formModel.Id = id;
            formModel.Title = program.Title;
            formModel.Description = program.Description;
            formModel.IdOfTrainer = program.IdOfTrainer;

            return formModel;
        }

        public async Task<FitnessProgramBIgViewModel> GetProgramAsync(Guid id)
        {
            var model = await data.FitnessPrograms.FirstOrDefaultAsync(x => x.Id == id);

            FitnessProgramBIgViewModel fitnessProgramBIgViewModel = new FitnessProgramBIgViewModel();

            fitnessProgramBIgViewModel.IssueDate = model.IssueDate;
            fitnessProgramBIgViewModel.Title = model.Title;
            fitnessProgramBIgViewModel.Description = model.Description;
            fitnessProgramBIgViewModel.Id = id;

            return fitnessProgramBIgViewModel;

        }

        public async Task<TrainerBigViewModel> GetTrainer(Guid id)
        {
            TrainerBigViewModel? trainerViewModel = await data.Users.Select(x => new TrainerBigViewModel()
            {
                TelephoneNumber = x.TelephoneNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Address = x.Address,
                Age = x.Age,
                Id = id,
            })!.FirstOrDefaultAsync(x => x.Id == id)!;


            Trainer? trainer = await data.Trainers.FirstOrDefaultAsync(x => x.Id == id);

            trainerViewModel!.Biography = trainer!.Biography;

            return trainerViewModel;
        }

        public async Task<bool> IsTrainer(Guid id)
        {
            bool result = await data.Trainers.AnyAsync(x => x.Id == id);

            return result;
        }
    }
}
