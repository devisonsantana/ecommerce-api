using Ecommerce.Application.Common.Queries;
using Ecommerce.Application.Common.Results;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Errors;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using FluentResults;

namespace Ecommerce.Application.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;

    public BrandService(IBrandRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<BrandResponse>> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
        {
            return Result.Fail(new NotFoundError(nameof(Brand), nameof(id), id));
        }

        var response = new BrandResponse(
            entity.Id, entity.Name, entity.Slug, entity.LogoUrl);
        return Result.Ok(response);
    }

    public async Task<Result<BrandResponse>> GetBySlugAsync(string slug)
    {
        var entity = await _repository.GetBySlugAsync(slug);

        if (entity is null)
        {
            return Result.Fail(new NotFoundError(nameof(Brand), nameof(slug), slug));
        }

        var response = new BrandResponse(
            entity.Id, entity.Name, entity.Slug, entity.LogoUrl);
        return Result.Ok(response);
    }

    public async Task<PagedResult<BrandResponse>> GetAllAsync(BrandQuery query)
    {
        var (items, totalItems) = await _repository.GetAllAsync(query);

        var brands = items.Select(b => new BrandResponse(
            b.Id, b.Name, b.Slug, b.LogoUrl));

        var result = new PagedResult<BrandResponse>(
            query.Page, query.PageSize, totalItems, brands);

        return result;
    }

    public async Task<Result<BrandCreateResponse>> CreateAsync(
        BrandCreateRequest brandRequest)
    {
        var brand = new Brand(brandRequest.Name, brandRequest.LogoUrl);

        var existingEntity = await _repository.GetBySlugAsync(brand.Slug);

        if (existingEntity is not null)
        {
            return Result.Fail(
                new ConflictError(
                    nameof(Brand), nameof(brandRequest.Name), brandRequest.Name)
            );
        }

        await _repository.AddAsync(brand);
        await _repository.SaveChangesAsync();

        var response = new BrandCreateResponse(
            brand.Id, brand.Name, brand.Slug, brand.LogoUrl);

        return Result.Ok(response);
    }

    public async Task<BulkResult<BrandCreateResponse>> CreateBulkAsync(
        IEnumerable<BrandCreateRequest> brandsRequest)
    {
        var slugsToInsert = brandsRequest.Select(b => SlugHelper.Generate(b.Name)).ToHashSet();
        var existingSlugs = (await _repository.GetExistingSlugs(slugsToInsert)).ToHashSet();

        var failed = new List<string>();
        var succeeded = new List<Brand>();

        foreach (var request in brandsRequest)
        {
            var slug = SlugHelper.Generate(request.Name);
            if (existingSlugs.Contains(slug))
            {
                failed.Add($"{nameof(Brand)} with {nameof(request.Name)} '{request.Name}' already exists.");
                continue;
            }
            var brand = new Brand(request.Name, request.LogoUrl);
            succeeded.Add(brand);

            existingSlugs.Add(slug);
        }

        if (succeeded.Count > 0)
        {
            await _repository.AddRangeAsync(succeeded);
            await _repository.SaveChangesAsync();
        }

        var created = succeeded.Select(b => new BrandCreateResponse(b.Id, b.Name, b.Slug, b.LogoUrl));
        return new(created, failed);
    }

    public async Task<Result> UpdateAsync(long id, BrandUpdateRequest brandRequest)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result.Fail(new NotFoundError(nameof(Brand), nameof(id), id));
        }

        if (!string.IsNullOrWhiteSpace(brandRequest.Name))
        {
            var slugExists = SlugHelper.Generate(brandRequest.Name);
            var existsSlug = await _repository.GetBySlugAsync(slugExists);
            if (existsSlug is not null && existsSlug.Id != id)
            {
                return Result.Fail(new ConflictError(nameof(Brand), nameof(id), brandRequest.Name));
            }
            entity.UpdateName(brandRequest.Name);
        }

        if (!string.IsNullOrWhiteSpace(brandRequest.LogoUrl))
        {
            entity.UpdateLogoUrl(brandRequest.LogoUrl);
        }

        await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
        {
            return Result.Fail(new NotFoundError(nameof(Brand), nameof(id), id));
        }

        await _repository.RemoveAsync(entity);
        await _repository.SaveChangesAsync();

        return Result.Ok();
    }

}