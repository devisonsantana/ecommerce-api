using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interfaces;

public interface IBrandRepository : IRepository<Brand>
{

    Task<IEnumerable<Brand>> GetByNameAsync(string name);
    Task<Brand?> GetBySlugAsync(string slug);
}