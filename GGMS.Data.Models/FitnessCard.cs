using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMS.Data.Models
{
    public class FitnessCard
    {
        public FitnessCard()
        {
            Id = Guid.NewGuid();
            IssueDate = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public Gym Gym { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Gym))]
        public Guid GymId { get; set; }

        [Required]
        public DateTime IssueDate { get; set; }

        [Required]
        public DateTime ExpiringDate { get; set; }
    }
}
