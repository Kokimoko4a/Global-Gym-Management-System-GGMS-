
using GGMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        public DbSet<Trainer> Trainers { get; set; } = null!;

        public DbSet<UserFitnessProgram> UserFitnessPrograms { get; set; } = null!;

        public DbSet<Gym> Gyms { get; set; } = null!;

        public DbSet<GymOwner> GymOwners { get; set; } = null!;




        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(ApplicationDbContext)) ??
                                    Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);


            // Ensure the Id is mapped correctly as a Guid
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("uniqueidentifier");
            });
        }
    }
}
