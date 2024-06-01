using GGMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMS.Data.Configurations
{
    internal class UserFitnessProgramEntityConfiguration : IEntityTypeConfiguration<UserFitnessProgram>
    {
        public void Configure(EntityTypeBuilder<UserFitnessProgram> builder)
        {
            builder.HasKey(x => new { x.UserId, x.FitnessProgramId });

            builder.HasOne(x => x.User)
                .WithMany(x => x.FitnessPrograms)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.FitnessProgram)
                .WithMany(x => x.FitnessProgramUsers)
                .HasForeignKey(x => x.FitnessProgramId)
                     .OnDelete(DeleteBehavior.Restrict); ;
        }
    }
}
