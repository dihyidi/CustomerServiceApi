using CustomerService.Domain.Models;
using CustomerService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories;

public class BusinessLocationRepository : CrudRepository<BusinessLocation>, IBusinessLocationRepository
{
    public BusinessLocationRepository(CustomerServiceDbContext dbContext) : base(dbContext)
    {
    }

    public Task<BusinessLocation> GetLocationWithEmployeeById(int id)
    {
        return DbContext.BusinessLocations
            .Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<BusinessLocation>> GetLocationsByCustomerId(int customerId)
    {
        return DbContext.BusinessLocations
            .Include(x => x.Customer)
            .Where(x => x.Customer.Id == customerId)
            .ToListAsync();
    }

    public async Task<List<Employee>> GetEmployeesByBusinessLocationId(int locationId)
    {
        var location = await DbContext.BusinessLocations
            .Include(x => x.Employees)
            .FirstOrDefaultAsync(x => x.Id == locationId);

        return location.Employees;
    }
}