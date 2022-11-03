using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mullayon.Core;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                if (isDevelopment)
                {
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

                }
                else
                {
                    options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
                }
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                // options.EnableSensitiveDataLogging();

            }
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}