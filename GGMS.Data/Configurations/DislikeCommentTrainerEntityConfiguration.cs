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
    public class DislikeCommentTrainerEntityConfiguration : IEntityTypeConfiguration<DislikeCommentTrainer>
    {
        public void Configure(EntityTypeBuilder<DislikeCommentTrainer> builder)
        {
            builder.HasOne(x => x.Comment)
                 .WithMany(x => x.DisLikes)
                 .HasForeignKey(x => x.CommentId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
