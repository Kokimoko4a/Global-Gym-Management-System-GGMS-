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
    public class GymOwerEntityConfiguration : IEntityTypeConfiguration<GymOwner>
    {
        public void Configure(EntityTypeBuilder<GymOwner> builder)
        {
            builder.HasMany(x => x.Gyms)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId);
        }
    }
}
