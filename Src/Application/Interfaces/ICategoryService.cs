using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Common;
using FluentResults;

namespace Ecommerce.Application.Interfaces;

public interface ICategoryService
{
    Task<Result<CategoryResponse>> GetByIdAsync(long id);
    Task<Result<CategoryResponse>> GetByNameAsync(string name);
    Task<IEnumerable<CategoryResponse>> GetAllAsync();
    Task<Result<CategoryCreateResponse>> CreateAsync(
        CategoryCreateRequest categoryRequest);
    Task<BulkResult<CategoryCreateResponse>> CreateBulkAsync(
        IEnumerable<CategoryCreateRequest> categoriesRequest);
    Task<Result> UpdateAsync(long id, CategoryUpdateRequest categoryRequest);
    Task<Result> DeleteAsync(long id);
}