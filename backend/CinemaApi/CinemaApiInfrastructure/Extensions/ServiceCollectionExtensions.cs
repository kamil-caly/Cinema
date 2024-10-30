using CinemaApiInfrastructure.Persistence;
using CinemaApiInfrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}
