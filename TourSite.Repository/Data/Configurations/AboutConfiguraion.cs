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
    public class AboutConfiguraion : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.ImageCover)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(a => a.ReferneceName)
                .IsRequired()
                .HasMaxLength(100);
  

            // One-to-One relationship with AboutTranslation
            builder.HasMany(a => a.AboutTranslations)
                   .WithOne(at => at.About)
                   .HasForeignKey(at => at.AboutId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
