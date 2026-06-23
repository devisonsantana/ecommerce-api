using Ecommerce.Application.Common.Results;
using Ecommerce.Application.Common.Queries;
using Ecommerce.Application.DTOs;
using FluentResults;

namespace Ecommerce.Application.Interfaces;

public interface ICategoryService
{
    Task<Result<CategoryResponse>> GetByIdAsync(long id);
    Task<Result<CategoryResponse>> GetByNameAsync(string name);
    Task<PagedResult<CategoryResponse>> GetAllAsync(CategoryQuery query);
    Task<Result<CategoryCreateResponse>> CreateAsync(
        CategoryCreateRequest categoryRequest);
    Task<BulkResult<CategoryCreateResponse>> CreateBulkAsync(
        IEnumerable<CategoryCreateRequest> categoriesRequest);
    Task<Result> UpdateAsync(long id, CategoryUpdateRequest categoryRequest);
    Task<Result> DeleteAsync(long id);
}