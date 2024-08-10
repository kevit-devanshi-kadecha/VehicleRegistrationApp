using Microsoft.EntityFrameworkCore;
using VehicleRegistration.Core.Interfaces;
using VehicleRegistration.Infrastructure;
using VehicleRegistration.Infrastructure.Services;

namespace VehicleRegistration.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped<IUserService, UserService>();

            // Service for Jwt Token 
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
