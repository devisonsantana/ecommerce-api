using Ecommerce.Application.Common.Queries;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    public async Task<Category?> GetByNameAsync(string name)
        => await Db
            .FirstOrDefaultAsync(c => c.Name == name);

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await Db.ToListAsync();

    public async Task<(IEnumerable<Category> Items, int TotalItems)> GetAllAsync(
        CategoryQuery categoryQuery)
    {
        var query = Db.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(categoryQuery.Name))
        {
            query = query.Where(c => c.Name.Contains(categoryQuery.Name));
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Id)
            .Skip((categoryQuery.Page - 1) * categoryQuery.PageSize)
            .Take(categoryQuery.PageSize)
            .ToListAsync();

        return (items, totalItems);
    }
}