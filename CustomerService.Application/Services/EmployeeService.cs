using CustomerService.Application.Mapping;
using CustomerService.Application.Models;
using CustomerService.Application.Services.Interfaces;
using CustomerService.Domain.Repositories;

namespace CustomerService.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IBusinessLocationRepository businessLocationRepository;
    private readonly IEmployeeRepository employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository,
        IBusinessLocationRepository businessLocationRepository)
    {
        this.employeeRepository = employeeRepository;
        this.businessLocationRepository = businessLocationRepository;
    }

    public async Task<IList<BusinessLocationModel>> GetAllBusinessLocationsByEmployeeId(int employeeId)
    {
        return (await employeeRepository.GetLocationsByEmployeeId(employeeId))?
            .Select(Mapper.MapBusinessLocationToModel).ToList();
    }

    public async Task AddBusinessLocationToEmployee(int employeeId, int locationId)
    {
        var employee = await employeeRepository.GetEmployeeByIdWithLocations(employeeId);
        if (employee is null) return;

        var location = await businessLocationRepository.GetByIdAsync(locationId);
        if (location is null) return;

        employee.BusinessLocations.Add(location);
        await employeeRepository.UpdateAsync(employee);
    }

    public async Task RemoveBusinessLocationFromEmployee(int employeeId, int locationId)
    {
        var employee = await employeeRepository.GetEmployeeByIdWithLocations(employeeId);
        if (employee is null) return;

        var idx = employee.BusinessLocations.FindIndex(x => x.Id == locationId);
        employee.BusinessLocations.RemoveAt(idx);
        await employeeRepository.UpdateAsync(employee);
    }
}