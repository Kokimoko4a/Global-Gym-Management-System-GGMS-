
using GGMS.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GGMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

            if (Database.IsRelational())
            {
                Database.EnsureCreated();
            }
        }

        public DbSet<FitnessProgram> FitnessPrograms { get; set; } = null!;
    }
}
