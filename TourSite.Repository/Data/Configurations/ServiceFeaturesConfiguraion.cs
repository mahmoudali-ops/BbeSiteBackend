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
    public class ServiceFeaturesConfiguraion : IEntityTypeConfiguration<ServiceFeatures>
    {
        public void Configure(EntityTypeBuilder<ServiceFeatures> builder)
        {
            builder.HasKey(at => at.Id);
            builder.HasMany(at => at.ServiceFeaturesTranslations)
                   .WithOne(att => att.ServiceFeatures)
                   .HasForeignKey(att => att.ServiceFeaturesId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
