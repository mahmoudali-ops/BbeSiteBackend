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
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(a => a.Id);

  

            builder.Property(a => a.ImageCover)
                .IsRequired()
                .HasMaxLength(250);
            builder.Property(a => a.ReferneceName)
                .IsRequired()
                .HasMaxLength(100);
     
            builder.Property(a => a.MetaDescription)
                .HasMaxLength(160);
            builder.Property(a => a.MetaKeyWords)
                .HasMaxLength(100);

              builder.HasMany(a => a.contactTranslation)
             .WithOne(at => at.Contact)
             .HasForeignKey(at => at.ContactId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
