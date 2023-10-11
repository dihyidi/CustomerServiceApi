using CustomerService.Domain.Models;

namespace CustomerService.Domain.Repositories;

public interface ICustomerRepository : ICrudRepository<Customer>
{
    Task<Customer> GetByCustomerNumberAsync(string number);

    Task<List<Customer>> GetByCustomersWithLocationsEmployeesAsync();

    Task<Customer> GetByCustomerWithLocationsEmployeesAsync(int id);
}