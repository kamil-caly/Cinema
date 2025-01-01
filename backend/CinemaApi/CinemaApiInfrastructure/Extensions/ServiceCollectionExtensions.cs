using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;
using CinemaApiInfrastructure.Repositories;
using CinemaApiInfrastructure.Seeders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CinemaApiInfrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CinemaApiDbContext>(options => options.UseMySql(
                configuration.GetConnectionString("Cinema"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("Cinema"))
            ));

            services.AddScoped<CinemaApiSeeder>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ISeanceRepository, SeanceRepository>();
            services.AddScoped<IHallRepository, HallRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddHttpContextAccessor();

            if (configuration["Jwt:Issuer"] == null || 
                configuration["Jwt:Audience"] == null || 
                configuration["Jwt:Key"] == null)
            {
                throw new Exception("Jwt:Issuer or Jwt:Audience or Jwt:Key is null");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "null"))
                };
            });

            return services;
        }
    }
}
