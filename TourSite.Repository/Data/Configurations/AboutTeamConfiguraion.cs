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
    public class AboutTeamConfiguraion : IEntityTypeConfiguration<AboutTeam>
    {
        public void Configure(EntityTypeBuilder<AboutTeam> builder)
        {
            builder.HasKey(at => at.Id);

            builder.HasMany(at => at.AboutTeamTranslations)
                   .WithOne(att => att.AboutTeam)
                   .HasForeignKey(att => att.AboutTeamId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
