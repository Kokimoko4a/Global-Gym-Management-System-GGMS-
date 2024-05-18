
using GGMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GGMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Ensure the Id is mapped correctly as a Guid
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("uniqueidentifier");
            });
        }
    }
}
