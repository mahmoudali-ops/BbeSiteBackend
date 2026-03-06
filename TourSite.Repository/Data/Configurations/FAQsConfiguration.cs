using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Repository.Data.Configurations
{
    public class FAQsConfiguration : IEntityTypeConfiguration<FAQs>
    {
        public void Configure(EntityTypeBuilder<FAQs> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.ImageCover).IsRequired().HasMaxLength(250);
            builder.Property(f => f.ReferneceName).IsRequired().HasMaxLength(150);

            builder.HasMany(at => at.fAQsTranslations)
        .WithOne(att => att.FAQs)
        .HasForeignKey(att => att.FAQsId)
        .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
