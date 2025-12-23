using BlogSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Presistence.Data.Configurations
{
    internal class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {

            builder.HasIndex(postTag => postTag.TagId);

            builder.HasIndex(postTag => postTag.PostId);
            //    builder.HasOne(x => x.Post)
            //        .WithMany(x => x.PostTags)
            //        .HasForeignKey(x => x.PostId);

            //    builder.HasOne(x => x.Tag)
            //        .WithMany()
            //        .HasForeignKey(x => x.TagId);
        }
    }
}
