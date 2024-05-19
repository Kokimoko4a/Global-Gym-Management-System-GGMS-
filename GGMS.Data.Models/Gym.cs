using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMS.Data.Models
{
    public class Gym
    {
        public Gym()
        {
            Id = Guid.NewGuid();
            FitnessPrograms = new HashSet<FitnessProgram>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public GymOwner Owner { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }

        [Required]
        public ICollection<FitnessProgram> FitnessPrograms { get; set; }
    }

}
