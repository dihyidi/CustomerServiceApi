using CustomerService.Domain.Models;
using CustomerService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories;

public abstract class CrudRepository<T> : ICrudRepository<T> where T : Entity
{
    protected readonly CustomerServiceDbContext DbContext;

    protected CrudRepository(CustomerServiceDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T> CreateAsync(T entity)
    {
        var added = DbContext.Set<T>().Add(entity);
        await DbContext.SaveChangesAsync();
        return added.Entity;
    }

    public Task UpdateAsync(T entity)
    {
        var updated = DbContext.Set<T>().Update(entity);
        return DbContext.SaveChangesAsync();
    }

    public Task<T> GetByIdAsync(int id)
    {
        return DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<T>> GetAllAsync()
    {
        return DbContext.Set<T>().ToListAsync();
    }

    public Task Delete(T entity)
    {
        var deleted = DbContext.Set<T>().Remove(entity);
        return DbContext.SaveChangesAsync();
    }
}