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
    public class CareerCardTranslationConfiguarion : IEntityTypeConfiguration<CareerCardTranslation>
    {
        public void Configure(EntityTypeBuilder<CareerCardTranslation> builder)
        {
            builder.HasKey(cct => cct.Id);
            builder.Property(cct => cct.Language)
                   .IsRequired();
            builder.Property(cct => cct.JobTitle)
                   .IsRequired()
                   .HasMaxLength(250);
            builder.Property(cct => cct.EmploymentType)
                   .IsRequired();
            builder.Property(cct => cct.SalaryFrom)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(cct => cct.SalaryTo)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(cct => cct.SalaryPeriod)
                   .IsRequired();
            builder.Property(cct => cct.Description)
                   .IsRequired();
            builder.Property(cct => cct.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");
            builder.Property(cct => cct.UpdatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");


            builder.HasOne(cct => cct.Career)
                   .WithMany(c => c.careerCardTranslations)
                   .HasForeignKey(cct => cct.CareerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
