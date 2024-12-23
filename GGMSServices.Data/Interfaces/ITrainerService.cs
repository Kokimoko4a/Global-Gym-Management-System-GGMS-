namespace GGMSServices.Data.Interfaces
{
    using GGMS.Web.ViewModels.FitnessProgram;
    using GGMS.Web.ViewModels.RequestToTrainer;
    using GGMS.Web.ViewModels.Trainer;
    using GGMS.Data.Models;

    public interface ITrainerService
    {
        public Task<bool> BecomeTrainer(TrainerFormModel trainerFormModel, Guid Id);

        public Task<bool> IsTrainer( Guid Id);

        public Task<bool> AddProgramAsync(FitnessProgramFormModel model);

        public Task<ICollection<FitnessProgramSmallViewModel>> AllProgramsAsync(Guid id);

        public Task<FitnessProgramBIgViewModel> GetProgramAsync(Guid id);

        public Task DeleteProgramAsync(Guid id);

        public Task EditProgramAsync(FitnessProgramFormModel model);

        public Task<FitnessProgramFormModel> GetProgramAsFormModel(Guid id);

        public Task<TrainerQueryModel> GetAllTrainersAsQueryModel(TrainerQueryModel queryModel);

        public Task<TrainerBigViewModel> GetTrainer(Guid id);

        public RequestToTrainerFormModel CreateRequestFormModel(Guid idOfClient, Guid idOfTrainer);

        public Task<RequestToTrainer> MakeRequestToTrainer(Guid trainerId, Guid userId, string message);

        public Task<IEnumerable<ApplicationUser>> GetAllClients(Guid id);

        public Task<ApplicationUser> GetSingleUserDataAsync(Guid id);

        public Task<List<FitnessProgram>> AssignProgramToClient(List<Guid> programIds, Guid userId);

        public Task SetMomentValueForTrainer(Guid id, Guid idOfClient);

        public Task<Trainer> GetTrainerBaseModel(Guid id);

        public Task<ApplicationUser> GetTrainerParalelUserRecord(Guid id);//gets user by trainer id ;)


    }
}
