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
    public class FitnessCardEntityConfiguration : IEntityTypeConfiguration<FitnessCard>
    {
        public void Configure(EntityTypeBuilder<FitnessCard> builder)
        {
            builder.HasOne(x => x.User)
                 .WithMany(x => x.FitnessCards)
                 .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Gym)
                .WithMany(x => x.FitnessCards)
                .HasForeignKey(x => x.GymId);
        }
    }
}
