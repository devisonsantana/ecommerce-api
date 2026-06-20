using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Persistence.Context;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });

        // Add services to the container.
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}