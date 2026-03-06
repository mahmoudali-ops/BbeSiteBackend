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
    public class HomeConfiguraion : IEntityTypeConfiguration<Home>
    {
        public void Configure(EntityTypeBuilder<Home> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasMany(a => a.HomeTranslation)
                     .WithOne(at => at.Home)
                     .HasForeignKey(at => at.HomeId)
                     .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
