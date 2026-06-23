using Ecommerce.Application.Common.Queries;
using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interfaces;

public interface IBrandRepository : IRepository<Brand>
{
    Task<Brand?> GetBySlugAsync(string slug);
    Task<IEnumerable<Brand>> GetAllAsync();
    Task<(IEnumerable<Brand> Items, int TotalItems)> GetAllAsync(
        BrandQuery brandQuery);
}