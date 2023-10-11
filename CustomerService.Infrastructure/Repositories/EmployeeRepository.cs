using CustomerService.Domain.Models;
using CustomerService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories;

public class EmployeeRepository : CrudRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(CustomerServiceDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Employee> GetEmployeeByIdWithLocations(int id)
    {
        return DbContext.Employees
            .Include(x => x.BusinessLocations)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<Employee>> GetEmployeesByCustomerId(int customerId)
    {
        return DbContext.Employees
            .Include(x => x.BusinessLocations)
            .ToListAsync();
    }

    public async Task<List<BusinessLocation>> GetLocationsByEmployeeId(int employeeId)
    {
        var employee = await DbContext.Employees
            .Include(x => x.BusinessLocations)
            .FirstOrDefaultAsync(x => x.Id == employeeId);

        return employee.BusinessLocations;
    }
}