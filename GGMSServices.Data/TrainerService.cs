namespace GGMSServices.Data
{
    using GGMS.Common;
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.FitnessProgram;
    using GGMS.Web.ViewModels.RequestToTrainer;
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

        public RequestToTrainerFormModel CreateRequestFormModel(Guid idOfClient, Guid idOfTrainer)
        {
            RequestToTrainerFormModel model = new RequestToTrainerFormModel();
            model.IdOfClient = idOfClient;
            model.IdOfTrainer = idOfTrainer;
            model.IsApproved = false;

            return model;
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

            allTrainers.Trainers = data.Trainers.Select(t => new TrainerSmallViewModel()
            {
                Id = t.Id,
            }).ToHashSet();

            foreach (var trainer in allTrainers.Trainers)
            {
                trainer.FirstName = data.Users.First(x => x.Id == trainer.Id).FirstName;
                trainer.LastName = data.Users.First(x => x.Id == trainer.Id).LastName;
            }


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
            ApplicationUser? user = await data.Users.FirstOrDefaultAsync(x => x.Id == id);



            Trainer? trainer = await data.Trainers
                .Include(x => x.Comments)
                    .ThenInclude(c => c.Author)
                .Include(x => x.Comments)
                    .ThenInclude(c => c.Likes)
                .FirstOrDefaultAsync(x => x.Id == id);



            TrainerBigViewModel trainerViewModel = new TrainerBigViewModel()
            {
                Id = id,
                Address = user.Address,
                Age = user.Age,
                Biography = trainer.Biography,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TelephoneNumber = user.TelephoneNumber,
                Comments = trainer.Comments,
            };

            return trainerViewModel;
        }

        public async Task<bool> IsTrainer(Guid id)
        {
            bool result = await data.Trainers.AnyAsync(x => x.Id == id);

            return result;
        }

        public async Task<RequestToTrainer> MakeRequestToTrainer(Guid trainerId, Guid userId, string message)
        {
            RequestToTrainer requestToTrainer = new RequestToTrainer();

            requestToTrainer.IdOfTrainer = trainerId;
            requestToTrainer.IdOfClient = userId;
            requestToTrainer.DecriptionOfRequest = message;

            await data.AddAsync(requestToTrainer);

            await data.SaveChangesAsync();

            return requestToTrainer;
        }


    }
}
