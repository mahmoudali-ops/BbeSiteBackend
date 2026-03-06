using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Repository.Data.Contexts
{
    public class BbeSiteDbContext : IdentityDbContext<User>
    {
        public BbeSiteDbContext(DbContextOptions<BbeSiteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ServiceCore> ServiceCores { get; set; }
        public DbSet<ServiceFeatures> ServiceFeatures { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutTeam> AboutTeams { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<FAQs> FAQs { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SocialElements> SocialElements { get; set; }
        public DbSet<BrandsImages> brandsImages { get; set; }
        public DbSet<Home> Homes { get; set; }




        // Translation DbSets
        public DbSet<ServiceCoreTranslation> ServiceCoreTranslations { get; set; }
        public DbSet<ServiceFeaturesTranslation> ServiceFeaturesTranslations{get; set;}
        public DbSet<AboutTeamTranslation> AboutTeamTranslations {get; set;}
        public DbSet<AboutTranslation> AboutTranslations{get; set;}
        public DbSet<CareerCardTranslation> CareerCardTranslations{get; set;}
        public DbSet<ContactTranslation> ContactTranslations{get; set;}
        public DbSet<FAQsTranslation> FAQsTranslations{get; set;}
        public DbSet<HomeTranslation> HomeTranslations{get; set;}
        public DbSet<PriceCardTranslation> PriceCardTranslations{get; set;}



        












    }
}
