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
            return Result.Fail(new NotFoundError($"Category Id '{id}' not found."));
        }
        var response = new CategoryResponse(model.Id, model.Name);
        return Result.Ok(response);
    }

    public async Task<Result<CategoryResponse>> GetByNameAsync(string name)
    {
        var model = await _repository.GetByNameAsync(name);
        if (model is null)
        {
            return Result.Fail(new NotFoundError($"Category '{name}' not found."));
        }
        var response = new CategoryResponse(model.Id, model.Name);
        return Result.Ok(response);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(c => new CategoryResponse(c.Id, c.Name)).ToList();
    }

    public async Task<Result<CategoryCreateResponse>> CreateAsync(CategoryCreateRequest request)
    {
        var model = await _repository.GetByNameAsync(request.Name);
        if (model is Category)
        {
            return Result.Fail(new ConflictError($"Category '{request.Name}' already exists."));
        }
        var category = new Category(request.Name);
        await _repository.AddAsync(new Category(request.Name));
        await _repository.SaveChangesAsync();

        var response = new CategoryCreateResponse(category.Id, category.Name);
        return Result.Ok(response);
    }

    public async Task<BulkResult<CategoryCreateResponse>> CreateBulkAsync(IEnumerable<CategoryCreateRequest> requests)
    {
        var existing = await _repository.GetAllAsync();
        var existingNames = existing.Select(c => c.Name.ToLower()).ToHashSet();

        var succeeded = new List<CategoryCreateResponse>();
        var failed = new List<string>();

        foreach (var request in requests)
        {
            if (existingNames.Contains(request.Name.ToLower()))
            {
                failed.Add($"Category '{request.Name}' already exists.");
                continue;
            }

            var category = new Category(request.Name);
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
}