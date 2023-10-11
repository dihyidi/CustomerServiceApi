using CustomerService.Domain.Models;

namespace CustomerService.Domain.Repositories;

public interface IBusinessLocationRepository : ICrudRepository<BusinessLocation>
{
    Task<BusinessLocation> GetLocationWithEmployeeById(int id);

    Task<List<BusinessLocation>> GetLocationsByCustomerId(int customerId);

    public Task<List<Employee>> GetEmployeesByBusinessLocationId(int locationId);
}