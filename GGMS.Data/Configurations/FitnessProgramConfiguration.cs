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
    internal class FitnessProgramConfiguration : IEntityTypeConfiguration<FitnessProgram>
    {
        public void Configure(EntityTypeBuilder<FitnessProgram> builder)
        {
            builder.HasOne(x => x.Trainer)
                .WithMany(x => x.FitnessPrograms)
                .HasForeignKey(x => x.IdOfTrainer);

            builder
                .HasMany(x => x.FitnessProgramUsers)
                .WithOne(x => x.FitnessProgram)
                .HasForeignKey(x => x.FitnessProgramId);


        }
    }
}
