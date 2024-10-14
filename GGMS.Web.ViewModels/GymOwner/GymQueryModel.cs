namespace GGMS.Web.ViewModels.GymOwner
{
    public class GymQueryModel
    {
        public GymQueryModel()
        {
            GymSmallViewModels = new HashSet<GymSmallViewModel>();
        }

        public string KeyWords { get; set; } = null!;

        public int CurrentPage { get; set; } = 1;

        public int TotalCountOfPages { get; set; }

        public int GymsPerPage { get; set; } = 6;

        public int TotalGyms { get; set; }

        public ICollection<GymSmallViewModel> GymSmallViewModels { get; set; }
    }
}
