using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByNameAsync(string name);
    Task<Product?> GetBySlugAsync(string slug);
}