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
    public class GymEntityConfiguration : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            builder.HasMany(x => x.FitnessPrograms)
                .WithOne(x => x.Gym)
                .HasForeignKey(x => x.GymId);


        }
    }
}
