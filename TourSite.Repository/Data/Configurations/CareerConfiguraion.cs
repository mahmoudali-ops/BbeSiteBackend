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
    public class CareerConfiguraion : IEntityTypeConfiguration<Career>
    {
        public void Configure(EntityTypeBuilder<Career> builder)
        {
           builder.HasKey(c => c.Id);
              builder.Property(c => c.ReferneceName).IsRequired().HasMaxLength(255);
                builder.Property(c => c.ImageCover).IsRequired().HasMaxLength(255);
            builder.HasKey(c => c.Id);
                builder.Property(c => c.ReferneceName).IsRequired().HasMaxLength(255);
                builder.Property(c => c.ImageCover).IsRequired().HasMaxLength(255);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.ReferneceName).IsRequired().HasMaxLength(255);
            builder.Property(c => c.ImageCover).IsRequired().HasMaxLength(255);

            builder.HasMany(at => at.careerCardTranslations)
                   .WithOne(att => att.Career)
                   .HasForeignKey(att => att.CareerId)
                   .OnDelete(DeleteBehavior.Cascade);

     



        }
    }
}
