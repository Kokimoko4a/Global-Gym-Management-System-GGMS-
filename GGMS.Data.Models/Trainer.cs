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

        [Required]
        public string FirstName { get; set; } = null!;
        
        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; } = null!;

        public ICollection<FitnessProgram> FitnessPrograms { get; set; } = null!;



    }
}
