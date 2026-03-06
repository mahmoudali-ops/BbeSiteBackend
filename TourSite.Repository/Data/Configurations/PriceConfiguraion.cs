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
    public class PriceConfiguraion : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(at => at.Id);

            builder.HasMany(at => at.priceCardTranslations)
            .WithOne(att => att.Price)
            .HasForeignKey(att => att.PriceId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
