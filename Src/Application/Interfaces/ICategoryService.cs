using Ecommerce.Domain.Common;
using Ecommerce.Domain.Models;
using FluentResults;

namespace Ecommerce.Application.Interfaces;

public interface ICategoryService
{
    Task<Result<Category>> GetByIdAsync(long id);
    Task<Result<Category>> GetByNameAsync(string name);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Result<Category>> CreateAsync(Category category);
    Task<BulkResult<Category>> CreateBulkAsync(IEnumerable<Category> categories);
}