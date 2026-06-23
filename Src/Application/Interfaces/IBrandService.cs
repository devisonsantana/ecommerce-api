using Ecommerce.Application.Common.Queries;
using Ecommerce.Application.Common.Results;
using Ecommerce.Application.DTOs;
using FluentResults;

namespace Ecommerce.Application.Interfaces;

public interface IBrandService
{
    Task<Result<BrandResponse>> GetByIdAsync(long id);
    Task<Result<BrandResponse>> GetBySlugAsync(string slug);
    Task<PagedResult<BrandResponse>> GetAllAsync(BrandQuery query);
    Task<Result<BrandCreateResponse>> CreateAsync(BrandCreateRequest brandRequest);
    Task<BulkResult<BrandCreateResponse>> CreateBulkAsync(
        IEnumerable<BrandCreateRequest> brandsRequest);
    Task<Result> UpdateAsync(long id, BrandUpdateRequest brandRequest);
    Task<Result> DeleteAsync(long id);
}