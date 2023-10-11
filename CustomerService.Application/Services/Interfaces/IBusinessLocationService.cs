using CustomerService.Application.Models;

namespace CustomerService.Application.Services.Interfaces;

public interface IBusinessLocationService
{
    Task<IList<EmployeeModel>> GetAllEmployeesByBusinessLocationId(int businessLocationId);

    Task AddEmployeeToBusinessLocation(int locationId, int employeeId);

    Task RemoveEmployeeFromBusinessLocation(int locationId, int employeeId);
}