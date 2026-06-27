using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySlugAsync(string slug);
}