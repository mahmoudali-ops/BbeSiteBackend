using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Repository.Data.Contexts;

namespace TourSite.Repository.Data
{
    public static class BbeSiteDbContextSeed
    {
        public static async Task SeedAsync(BbeSiteDbContext context)
        {

            if (context.Abouts.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\about.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<About>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Abouts.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.AboutTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\aboutranslation.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<AboutTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.AboutTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.AboutTeams.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\aboutTeam.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<AboutTeam>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.AboutTeams.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.AboutTeamTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\aboutTeamTranslation.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<AboutTeamTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.AboutTeamTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.brandsImages.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\brangimages.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<BrandsImages>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.brandsImages.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Careers.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\career.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<Career>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Careers.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.CareerCardTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\careercardtranlation.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<CareerCardTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.CareerCardTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Contacts.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\contact.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<Contact>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Contacts.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.ContactTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\contacttranlation.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<ContactTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.ContactTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Emails.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\emails.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<Email>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Emails.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.FAQs.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\faq.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<FAQs>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.FAQs.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.FAQsTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\faqtranlaion.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<FAQsTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.FAQsTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Homes.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\home.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<Home>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Homes.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.HomeTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\hometranlations.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<HomeTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.HomeTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Prices.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\pricecard.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<Price>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Prices.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.PriceCardTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\pricecardtranlation.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<PriceCardTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.PriceCardTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Services.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\service.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<Service>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Services.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.ServiceCores.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\servicecore.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<ServiceCore>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.ServiceCores.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.ServiceCoreTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\servicecoreetranlation.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<ServiceCoreTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.ServiceCoreTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.ServiceFeatures.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\servicefeature.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<ServiceFeatures>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.ServiceFeatures.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.ServiceFeaturesTranslations.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\servicefeaturetranaltion.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<ServiceFeaturesTranslation>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.ServiceFeaturesTranslations.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.SocialElements.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\socailements.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<SocialElements>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.SocialElements.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }

            if (context.Users.Count() == 0)
            {
                // 1- Read data from file 
                var strings = File.ReadAllText(@"..\TourSite.Repository\Data\DataSeed\users.json");

                if (string.IsNullOrEmpty(strings))
                {
                    throw new ArgumentException("The Tours.json file is empty or not found.");
                }
                // 2- Deserialize the JSON data into a list of ProductBrand objects

                var tours = JsonSerializer.Deserialize<List<User>>(strings);

                if (tours is not null && tours.Count() > 0)
                {
                    await context.Users.AddRangeAsync(tours);

                    // 3- Save changes to the database

                    await context.SaveChangesAsync();
                }

            }


        }
    }
}
