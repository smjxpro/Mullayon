using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mullayon.Core;
using Mullayon.Infrastructure.Data;

namespace Mullayon.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => { options.UseSqlite(configuration.GetConnectionString("DefaultConnection")); }
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}