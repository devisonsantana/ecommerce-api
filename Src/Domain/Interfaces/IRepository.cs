namespace Ecommerce.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id);
    Task AddAsync(T model);
    Task UpdateAsync(T model);
    Task RemoveAsync(T model);
    Task SaveChangesAsync();
}