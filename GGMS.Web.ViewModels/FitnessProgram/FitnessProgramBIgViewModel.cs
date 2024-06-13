namespace GGMS.Web.ViewModels.FitnessProgram
{
    public class FitnessProgramBIgViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime IssueDate { get; set; }
    }
}
