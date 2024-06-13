namespace GGMS.Web.ViewModels.Trainer
{
    using System.ComponentModel.DataAnnotations;

    public class AllTrainers
    {
        public AllTrainers()
        {
            Trainers = new HashSet<TrainerSmallViewModel>();
        }

        [Required]
        public ICollection<TrainerSmallViewModel> Trainers { get; set; } = null!;
    }
}
