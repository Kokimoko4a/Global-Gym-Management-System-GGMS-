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
    public class LikeCommentTrainerEntityConfiguration : IEntityTypeConfiguration<LikeCommentTrainer>
    {
        public void Configure(EntityTypeBuilder<LikeCommentTrainer> builder)
        {
            builder.HasOne(x => x.Comment)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
