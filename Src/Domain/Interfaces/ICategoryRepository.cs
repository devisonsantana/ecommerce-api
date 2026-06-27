using Ecommerce.Application.Common.Queries;
using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
    Task<IEnumerable<string>> GetExistingNames(IEnumerable<string> names);
    Task<(IEnumerable<Category> Items, int TotalItems)> GetAllAsync(
        CategoryQuery query);
    Task AddRangeAsync(IEnumerable<Category> categories);
}