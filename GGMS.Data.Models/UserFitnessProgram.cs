using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMS.Data.Models
{
    public class UserFitnessProgram
    {
        [Required]
        public Guid UserId { get; set; } 

        [Required]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public Guid FitnessProgramId { get; set; } 

        [Required]
        public FitnessProgram FitnessProgram { get; set; } = null!;
    }
}
