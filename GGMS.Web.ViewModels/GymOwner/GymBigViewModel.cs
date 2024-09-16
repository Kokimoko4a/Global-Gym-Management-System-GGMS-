namespace GGMS.Web.ViewModels.GymOwner
{
    public class GymBigViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string PhotoPaths { get; set; } = null!;
    }
}
