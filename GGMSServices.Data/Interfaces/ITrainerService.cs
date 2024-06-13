namespace GGMSServices.Data.Interfaces
{
    using GGMS.Web.ViewModels.FitnessProgram;
    using GGMS.Web.ViewModels.Trainer;

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

        public AllTrainers GetAllTrainers();

        public Task<TrainerBigViewModel> GetTrainer(Guid id);

    }
}
