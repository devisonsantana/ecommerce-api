using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Errors;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using FluentResults;

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
        var model = await _repository.GetByIdAsync(id);
        if (model is null)
        {
            return Result.Fail(new NotFoundError(nameof(Category), nameof(id), id));
        }
        var response = new CategoryResponse(model.Id, model.Name);
        return Result.Ok(response);
    }

    public async Task<Result<CategoryResponse>> GetByNameAsync(string name)
    {
        var model = await _repository.GetByNameAsync(name);
        if (model is null)
        {
            return Result.Fail(new NotFoundError(nameof(Category), nameof(name), name));
        }
        var response = new CategoryResponse(model.Id, model.Name);
        return Result.Ok(response);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(c => new CategoryResponse(c.Id, c.Name)).ToList();
    }

    public async Task<Result<CategoryCreateResponse>> CreateAsync(
        CategoryCreateRequest categoryRequest)
    {
        var model = await _repository.GetByNameAsync(categoryRequest.Name);
        if (model is Category)
        {
            return Result.Fail(new ConflictError(
                nameof(Category), nameof(categoryRequest.Name), categoryRequest.Name));
        }
        var category = new Category(categoryRequest.Name.ToLower());
        await _repository.AddAsync(new Category(categoryRequest.Name));
        await _repository.SaveChangesAsync();

        var response = new CategoryCreateResponse(category.Id, category.Name);
        return Result.Ok(response);
    }

    public async Task<BulkResult<CategoryCreateResponse>> CreateBulkAsync(
        IEnumerable<CategoryCreateRequest> categoriesRequest)
    {
        var existing = await _repository.GetAllAsync();
        var existingNames = existing.Select(c => c.Name.ToLower()).ToHashSet();

        var succeeded = new List<CategoryCreateResponse>();
        var failed = new List<string>();

        foreach (var request in categoriesRequest)
        {
            if (existingNames.Contains(request.Name.ToLower()))
            {
                failed.Add($"{nameof(Category)} with {nameof(request.Name)} '{request.Name}' already exists.");
                continue;
            }

            var category = new Category(request.Name.ToLower());
            await _repository.AddAsync(category);
            existingNames.Add(request.Name.ToLower());
            succeeded.Add(new(category.Id, category.Name));
        }

        if (succeeded.Any())
        {
            await _repository.SaveChangesAsync();
        }

        return new(succeeded, failed);
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

        entity.UpdateName(categoryRequest.Name.ToLower());

        await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(long id)
    {
        var exists = await _repository.GetByIdAsync(id);

        if (exists is null)
            return Result.Fail(new NotFoundError(nameof(Category), nameof(id), id));

        await _repository.RemoveAsync(exists);
        await _repository.SaveChangesAsync();

        return Result.Ok();
    }
}