using CustomerService.Application.Mapping;
using CustomerService.Application.Models;
using CustomerService.Application.Services.Interfaces;
using CustomerService.Domain.Repositories;

namespace CustomerService.Application.Services;

public class BusinessLocationService : IBusinessLocationService
{
    private readonly IBusinessLocationRepository businessLocationRepository;
    private readonly IEmployeeRepository employeeRepository;

    public BusinessLocationService(IBusinessLocationRepository businessLocationRepository,
        IEmployeeRepository employeeRepository)
    {
        this.businessLocationRepository = businessLocationRepository;
        this.employeeRepository = employeeRepository;
    }

    public async Task<IList<EmployeeModel>> GetAllEmployeesByBusinessLocationId(int businessLocationId)
    {
        return (await businessLocationRepository.GetEmployeesByBusinessLocationId(businessLocationId))?
            .Select(Mapper.MapEmployeeToModel).ToList();
    }

    public async Task AddEmployeeToBusinessLocation(int locationId, int employeeId)
    {
        var location = await businessLocationRepository.GetLocationWithEmployeeById(locationId);
        if (location is null) return;

        var employee = await employeeRepository.GetByIdAsync(employeeId);
        if (employee is null) return;

        location.Employees.Add(employee);
        await businessLocationRepository.UpdateAsync(location);
    }

    public async Task RemoveEmployeeFromBusinessLocation(int locationId, int employeeId)
    {
        var location = await businessLocationRepository.GetLocationWithEmployeeById(locationId);
        if (location is null) return;

        var idx = location.Employees.FindIndex(x => x.Id == employeeId);
        location.Employees.RemoveAt(idx);
        await businessLocationRepository.UpdateAsync(location);
    }
}