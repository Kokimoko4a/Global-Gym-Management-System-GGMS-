﻿namespace GGMSServices.Data
{
    using GGMS.Data;
    using GGMS.Data.Models;
    using GGMS.Web.ViewModels.FitnessProgram;
    using GGMS.Web.ViewModels.GymOwner;
    using GGMS.Web.ViewModels.RequestToTrainer;
    using GGMS.Web.ViewModels.Trainer;
    using GGMSServices.Data.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

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

        public async Task<List<FitnessProgram>> AssignProgramToClient(List<Guid> programIds, Guid userId)
        {
           

            List<FitnessProgram> fitnessPrograms = data.FitnessPrograms.Where(x => programIds.Contains(x.Id)).ToList();

            ApplicationUser applicationUser = await data.Users.Include(x => x.FitnessPrograms).FirstOrDefaultAsync(x => x.Id == userId)!;

           

            foreach (var program in fitnessPrograms)
            {
                if (applicationUser.FitnessPrograms.FirstOrDefault(x => x.FitnessProgramId == program.Id) == null)
                {

                    UserFitnessProgram userFitnessProgram = new UserFitnessProgram()
                    {
                        FitnessProgram = program,
                        FitnessProgramId = program.Id,
                        User = applicationUser!,
                        UserId = userId
                    };



                    applicationUser!.FitnessPrograms.Add(userFitnessProgram);
                }
            }

            await data.SaveChangesAsync();

            return fitnessPrograms;
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

        public async Task<IEnumerable<ApplicationUser>> GetAllClients(Guid id)
        {
            Trainer trainer = await data.Trainers.Include(x => x.Clients).FirstOrDefaultAsync(x => x.Id == id)!;

            var ani = trainer.Clients.FirstOrDefault(x => x.Email.Contains("client"));

         

            return trainer!.Clients;
        }

 

        public async Task<TrainerQueryModel> GetAllTrainersAsQueryModel( TrainerQueryModel queryModel)
        {
            int recordsToSkip = Math.Abs((queryModel.CurrentPage - 1) * 6);

            


            if (!string.IsNullOrWhiteSpace(queryModel.KeyWords))
            {

                List<ApplicationUser> usersFromDbFiltered = await data.Users.
                    Where(x => x.FirstName.Contains(queryModel.KeyWords) || x.Address.Contains(queryModel.KeyWords) || x.LastName.Contains(queryModel.KeyWords)).Skip(recordsToSkip)
                    .ToListAsync();

                queryModel.TrainerSmallViewModels = usersFromDbFiltered.Select(x => new TrainerSmallViewModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Id = x.Id,
                    PathToImage = x.PathToImage
                }).ToHashSet();

                return queryModel;

            }

            List<ApplicationUser> usersFromDb = await data.Users.Skip(recordsToSkip).Take(queryModel.TrainersPerPage).ToListAsync();



            queryModel.TrainerSmallViewModels = usersFromDb.Select(x => new TrainerSmallViewModel()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
                PathToImage = x.PathToImage
            }).ToHashSet();

            return queryModel;
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

        public async Task<ApplicationUser> GetSingleUserDataAsync(Guid id)
        {
            return await data.Users.FirstOrDefaultAsync(x => x.Id == id)!;
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
                PathToImage = user.PathToImage
            };

            return trainerViewModel;
        }

        public async Task<Trainer> GetTrainerBaseModel(Guid id) => await data.Trainers.FirstAsync(x => x.Id == id);

        public async Task<ApplicationUser> GetTrainerParalelUserRecord(Guid id)
        {
            return await data.Users.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task SetMomentValueForTrainer(Guid id, Guid momentIdOfClient)
        {
           Trainer trainer = await data.Trainers.FirstOrDefaultAsync(x => x.Id == id)!;

            trainer.IdOfClientCurrentlyWorkingWith = momentIdOfClient;

            await data.SaveChangesAsync();
        }
    }
}
