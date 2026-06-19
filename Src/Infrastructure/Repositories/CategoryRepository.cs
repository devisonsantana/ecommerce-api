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
            .FirstOrDefaultAsync(c => c.Name == name.ToLower());
}