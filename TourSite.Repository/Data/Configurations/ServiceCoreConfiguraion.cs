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
    public class ServiceCoreConfiguraion : IEntityTypeConfiguration<ServiceCore>
    {
        public void Configure(EntityTypeBuilder<ServiceCore> builder)
        {
            builder.HasKey(sc => sc.Id);
            builder.Property(sc => sc.ImageCover)
                   .IsRequired()
                   .HasMaxLength(250);
            builder.HasMany(at => at.ServiceCoreTranslations)
                   .WithOne(att => att.ServiceCore)
                   .HasForeignKey(att => att.ServiceCoreId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
