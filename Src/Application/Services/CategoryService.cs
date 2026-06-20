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

    public async Task<Result<Category>> GetByIdAsync(long id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model is null)
        {
            return Result.Fail(new NotFoundError($"Category Id '{id}' not found."));
        }
        return Result.Ok(model);
    }

    public async Task<Result<Category>> GetByNameAsync(string name)
    {
        var model = await _repository.GetByNameAsync(name);
        if (model is null)
        {
            return Result.Fail(new NotFoundError($"Category '{name}' not found."));
        }
        return Result.Ok(model);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Result<Category>> CreateAsync(Category category)
    {
        var model = await _repository.GetByNameAsync(category.Name);
        if (model is Category)
        {
            return Result.Fail(new ConflictError($"Category '{category.Name}' already exists."));
        }
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();

        return Result.Ok(category);
    }

    public async Task<BulkResult<Category>> CreateBulkAsync(IEnumerable<Category> categories)
    {
        var existing = await _repository.GetAllAsync();
        var existingNames = existing.Select(c => c.Name.ToLower()).ToHashSet();

        var succeeded = new List<Category>();
        var failed = new List<string>();

        foreach (var category in categories)
        {
            if (existingNames.Contains(category.Name.ToLower()))
            {
                failed.Add($"Category '{category.Name}' already exists.");
                continue;
            }

            await _repository.AddAsync(category);
            existingNames.Add(category.Name.ToLower());
            succeeded.Add(category);
        }

        if (succeeded.Any())
        {
            await _repository.SaveChangesAsync();
        }

        return new(succeeded, failed);
    }
}