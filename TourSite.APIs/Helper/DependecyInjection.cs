using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using TourSite.APIs.Errors;
using TourSite.Core;
using TourSite.Core.Entities;
using TourSite.Core.Mapping;
using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data.Contexts;
using TourSite.Repository.Repositories;
using TourSite.Service.Services.Abouts;
using TourSite.Service.Services.AboutTeams;
using TourSite.Service.Services.Auth;
using TourSite.Service.Services.BrandsImagess;
using TourSite.Service.Services.Cache;
using TourSite.Service.Services.Careers;
using TourSite.Service.Services.Contacts;
using TourSite.Service.Services.Emails;
using TourSite.Service.Services.FAQ;
using TourSite.Service.Services.Homes;
using TourSite.Service.Services.Prices;
using TourSite.Service.Services.Service;
using TourSite.Service.Services.ServiceCores;
using TourSite.Service.Services.ServiceFeature;
using TourSite.Service.Services.SocialELements;
using TourSite.Service.Services.Token;
using TourSite.Service.Services.Users;


namespace TourSite.APIs.Helper
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.IConnectionMultiplexer();
            services.AddBulitinService();
            services.AddSwaggerService();
            services.AddDbContextSerevice(configuration);
            services.AddUserDefinedSerevice();
            services.AddMappingSerevice(configuration);
            services.AddValidationErrorService(configuration);
            services.AddingIDentityService();
            services.AddingAuthenticationService(configuration);
            

            return services;
        }
        private static IServiceCollection AddBulitinService(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection AddDbContextSerevice(this IServiceCollection services, IConfiguration configuration)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddDbContext<BbeSiteDbContext>(options =>
                         options.UseSqlServer(configuration.GetConnectionString("Default")));

            return services;
        }
        private static IServiceCollection AddUserDefinedSerevice(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IFAQsService, FAQService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAboutServicecs, AboutService>();
            services.AddScoped<IAboutTeamService, AboutTeamService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IServiceService, ServicesService>();
            services.AddScoped<IBrandImagesService, BrandsImageService>();
            services.AddScoped<ISocialElemtsService, SocialELementsaService>();
            services.AddScoped<IPriceService, PriceService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IServiceCoreService, ServiceCoreService>();
            services.AddScoped<IServiceFeatureService, ServiceFeatureService>();
            services.AddScoped<ICareerService, CareerService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailSender_, EmailSender>();
            services.AddScoped<IHomeService, HomeService>();

            return services;
        }
        private static IServiceCollection AddMappingSerevice(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddAutoMapper(m => m.AddProfile(new FAQProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new UserProfile()));
            services.AddAutoMapper(m => m.AddProfile(new AboutProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new AboutTeamProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new ContactProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new ServiceProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new BrandImagesProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new SocialElemrntsProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new PriceProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new ServiceProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new ServiceFeatureProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new ServiceCoreProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new HomeProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new CareerProfile(configuration)));
            services.AddAutoMapper(m => m.AddProfile(new EmailsProfile()));




            return services;
        }
        private static IServiceCollection AddValidationErrorService(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<ApiBehaviorOptions>(options =>

                 options.InvalidModelStateResponseFactory = (actionContext) =>
                 {

                     var errors = actionContext.ModelState
                   .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                           .Select(x => x.ErrorMessage)
                             .ToArray();

                     var response = new ValidationErrorResponse()
                     {
                         errors = errors
                     };

                     return new BadRequestObjectResult(response);
                 }
                         );


            return services;
        }
        private static IServiceCollection IConnectionMultiplexer(this IServiceCollection services)
        {

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse("localhost:6379", true);
                configuration.AbortOnConnectFail = false; // لتجنب الانهيار لو السيرفر لسه بيبدأ
                return ConnectionMultiplexer.Connect(configuration);
            });
            return services;
        }
        private static IServiceCollection AddingIDentityService(this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<BbeSiteDbContext>();
            return services;
        }
        private static IServiceCollection AddingAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Jwt not cookie
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //unauth
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>  // Validate
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;  // local 
                                                       //   validate data of token when it return
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true, // ✅ لازم
                    ClockSkew = TimeSpan.Zero, // ✅ علشان ما يضيفش 5 دقايق سماح
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"])),
                };

                // 👇 هنا المهم جداً: اقرأ الـ JWT من الكوكي HttpOnly
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("jwt"))
                        {
                            context.Token = context.Request.Cookies["jwt"];
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            return services;

        }

    }
}
