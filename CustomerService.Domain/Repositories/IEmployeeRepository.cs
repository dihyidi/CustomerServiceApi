using CustomerService.Domain.Models;

namespace CustomerService.Domain.Repositories;

public interface IEmployeeRepository : ICrudRepository<Employee>
{
    public Task<Employee> GetEmployeeByIdWithLocations(int id);

    public Task<List<Employee>> GetEmployeesByCustomerId(int customerId);

    Task<List<BusinessLocation>> GetLocationsByEmployeeId(int employeeId);
}