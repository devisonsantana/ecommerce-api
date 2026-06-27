using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Errors;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using FluentResults;
using Ecommerce.Application.Common.Results;
using Ecommerce.Application.Common.Queries;

namespace Ecommerce.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CategoryResponse>> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
        {
            return Result.Fail(new NotFoundError(nameof(Category), nameof(id), id));
        }
        var response = new CategoryResponse(entity.Id, entity.Name);
        return Result.Ok(response);
    }

    public async Task<Result<CategoryResponse>> GetByNameAsync(string name)
    {
        var entity = await _repository.GetByNameAsync(name);
        if (entity is null)
        {
            return Result.Fail(new NotFoundError(nameof(Category), nameof(name), name));
        }
        var response = new CategoryResponse(entity.Id, entity.Name);
        return Result.Ok(response);
    }

    public async Task<PagedResult<CategoryResponse>> GetAllAsync(CategoryQuery query)
    {
        var (items, totalItems) = await _repository.GetAllAsync(query);

        var categories = items.Select(c => new CategoryResponse(c.Id, c.Name)).ToList();

        var pagedResult = new PagedResult<CategoryResponse>(
            query.Page, query.PageSize, totalItems, categories);

        return pagedResult;
    }

    public async Task<Result<CategoryCreateResponse>> CreateAsync(
        CategoryCreateRequest categoryRequest)
    {
        var entity = await _repository.GetByNameAsync(categoryRequest.Name);
        if (entity is not null)
        {
            return Result.Fail(new ConflictError(
                nameof(Category), nameof(categoryRequest.Name), categoryRequest.Name));
        }
        var category = new Category(categoryRequest.Name);
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        var response = new CategoryCreateResponse(category.Id, category.Name);
        return Result.Ok(response);
    }

    public async Task<BulkResult<CategoryCreateResponse>> CreateBulkAsync(
        IEnumerable<CategoryCreateRequest> categoriesRequest)
    {
        var namesToInsert = categoriesRequest.Select(n => n.Name).ToHashSet();
        var existingNames = (await _repository.GetExistingNames(namesToInsert)).ToHashSet();

        var succeeded = new List<Category>();
        var failed = new List<string>();

        foreach (var request in categoriesRequest)
        {
            if (existingNames.Contains(request.Name))
            {
                failed.Add($"{nameof(Category)} with {nameof(request.Name)} '{request.Name}' already exists.");
                continue;
            }

            var category = new Category(request.Name);
            succeeded.Add(category);

            existingNames.Add(request.Name);
        }

        if (succeeded.Count > 0)
        {
            await _repository.AddRangeAsync(succeeded);
            await _repository.SaveChangesAsync();
        }
        var created = succeeded.Select(c => new CategoryCreateResponse(c.Id, c.Name));
        return new(created, failed);
    }

    public async Task<Result> UpdateAsync(long id, CategoryUpdateRequest categoryRequest)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return Result.Fail(new NotFoundError(nameof(Category), nameof(id), id));

        var existing = await _repository.GetByNameAsync(categoryRequest.Name);

        if (existing is not null && existing.Id != id)
            return Result.Fail(new ConflictError(
                    nameof(Category), nameof(categoryRequest.Name), categoryRequest.Name));

        entity.UpdateName(categoryRequest.Name);

        await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return Result.Fail(new NotFoundError(nameof(Category), nameof(id), id));

        await _repository.RemoveAsync(entity);
        await _repository.SaveChangesAsync();

        return Result.Ok();
    }
}