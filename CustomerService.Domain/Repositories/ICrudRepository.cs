using CustomerService.Domain.Models;

namespace CustomerService.Domain.Repositories;

public interface ICrudRepository<T> where T : Entity
{
    Task<T> CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task<T> GetByIdAsync(int id);

    Task<List<T>> GetAllAsync();

    Task Delete(T entity);
}