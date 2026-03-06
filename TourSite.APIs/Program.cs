
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourSite.APIs.Errors;
using TourSite.APIs.Helper;
using TourSite.APIs.MidleWare;
using TourSite.Core;

using TourSite.Core.Servicies.Contract;
using TourSite.Repository.Data;
using TourSite.Repository.Data.Contexts;
using TourSite.Repository.Repositories;


namespace TourSite.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ CORS مفتوحة لأي حد (مؤقتًا)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("OpenCors", policy =>
                {
                    policy
                        .WithOrigins(
                            "https://bbesocial.com",
                            "http://localhost:4200",
                            "https://www.bbesocial.com" // 👈 ضيف دي

                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // 👈 مهم جدًا
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ===== DI =====
            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();
            await app.UseConfigurationMiddleWare();


            // ===== Middlewares Order (الترتيب الصح) =====
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("OpenCors"); // 👈 قبل Auth

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionMidleWare>();

            app.MapControllers();

            app.Run();
        }
    }
}
