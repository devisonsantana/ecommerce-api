using Ecommerce.Domain.Interfaces;
using Ecommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> Db;

    protected Repository(AppDbContext context)
    {
        Context = context;
        Db = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(long id)
        => await Db.FindAsync(id);

    public async Task AddAsync(T model)
        => await Db.AddAsync(model);

    public Task UpdateAsync(T model)
    {
        Db.Update(model);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(T model)
    {
        Db.Remove(model);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
        => await Context.SaveChangesAsync();
}