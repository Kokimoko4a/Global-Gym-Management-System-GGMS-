using GGMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GGMS.Data.Configurations
{
    public class CommentTrainerEntityConfiguration : IEntityTypeConfiguration<CommentTrainer>
    {
        public void Configure(EntityTypeBuilder<CommentTrainer> builder)
        {
            builder.HasMany(x => x.Likes)
                 .WithOne(x => x.Comment)
                 .HasForeignKey(x => x.CommentId);

            builder.HasMany(x => x.DisLikes)
                 .WithOne(x => x.Comment)
                 .HasForeignKey(x => x.CommentId);
        }
    }
}
