namespace GGMS.Web.ViewModels.Trainer
{
    public class TrainerBigViewModel
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Biography { get; set; } = null!;

        public int Age { get; set; }

        public string TelephoneNumber { get; set; } = null!;

        public string Address { get; set; } = null!;
    }
}
