namespace GGMS.Web.ViewModels.Trainer
{
    public class TrainerQueryModel
    {

        public string KeyWords { get; set; } = null!;

        public int CurrentPage { get; set; } = 1;

        public int TotalCountOfPages { get; set; }

        public int TrainersPerPage { get; set; } = 6;

        public int TotalTrainers { get; set; }

        public ICollection<TrainerSmallViewModel> TrainerSmallViewModels { get; set; }
    }
}
