using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;

namespace Ecommerce.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        return services;
    }
}