using Ecommerce.Application.Common.Queries;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public class BrandRepository : Repository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context) { }

    public async Task<Brand?> GetBySlugAsync(string slug)
        => await Db.FirstOrDefaultAsync(b => b.Slug == slug);

    public async Task<IEnumerable<string>> GetExistingSlugs(IEnumerable<string> slugs)
        => await Db
            .Where(b => slugs.Contains(b.Slug))
            .Select(b => b.Slug)
            .ToHashSetAsync();

    public async Task<(IEnumerable<Brand> Items, int TotalItems)> GetAllAsync(
        BrandQuery brandQuery)
    {
        var query = Db.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(brandQuery.Name))
        {
            query = Db.Where(b => b.Name.ToLower().Contains(brandQuery.Name.ToLower()));
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderBy(b => b.Id)
            .Skip((brandQuery.Page - 1) * brandQuery.PageSize)
            .Take(brandQuery.PageSize)
            .ToListAsync();

        return (items, totalItems);
    }

    public async Task AddRangeAsync(IEnumerable<Brand> brands)
        => await Db.AddRangeAsync(brands);
}