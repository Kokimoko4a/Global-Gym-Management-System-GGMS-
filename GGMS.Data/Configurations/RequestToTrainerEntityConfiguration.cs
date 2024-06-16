using GGMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GGMS.Data.Configurations
{
    public class RequestToTrainerEntityConfiguration : IEntityTypeConfiguration<RequestToTrainer>
    {

        public void Configure(EntityTypeBuilder<RequestToTrainer> builder)
        {
            builder.HasOne(x => x.Trainer)
                 .WithMany(x => x.Requests)
                 .HasForeignKey(x => x.IdOfTrainer);


            builder.HasOne(x => x.Client)
             .WithMany(x => x.Requests)
            .HasForeignKey(x => x.IdOfClient);
        }
    }
}
