using CustomerService.Application.Models;

namespace CustomerService.Application.Services.Interfaces;

public interface IEmployeeService
{
    Task<IList<BusinessLocationModel>> GetAllBusinessLocationsByEmployeeId(int employeeId);

    Task AddBusinessLocationToEmployee(int employeeId, int locationId);

    Task RemoveBusinessLocationFromEmployee(int employeeId, int locationId);
}