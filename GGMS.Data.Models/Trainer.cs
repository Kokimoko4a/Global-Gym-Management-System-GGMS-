using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMS.Data.Models
{
    public class Trainer
    {
        public Trainer()
        {
            FitnessPrograms = new HashSet<FitnessProgram>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        public ICollection<FitnessProgram> FitnessPrograms { get; set; } = null!;
    }
}
