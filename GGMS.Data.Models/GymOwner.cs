namespace GGMS.Data.Models
{

    using System.ComponentModel.DataAnnotations;
    public class GymOwner
    {
        public GymOwner()
        {        
            Gyms = new HashSet<Gym>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public ICollection<Gym> Gyms { get; set; } = null!;
    }
}
