using CustomerService.Domain.Models;
using CustomerService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure.Repositories;

public class CustomerRepository : CrudRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(CustomerServiceDbContext dbContext) : base(dbContext)
    {
    }

    public Task<Customer> GetByCustomerNumberAsync(string number)
    {
        return DbContext.Customers.FirstOrDefaultAsync(x => x.CustomerNumber == number);
    }

    public Task<List<Customer>> GetByCustomersWithLocationsEmployeesAsync()
    {
        return DbContext.Customers
            .Include(x => x.Employees)
            .Include(x => x.BusinessLocations)
            .ToListAsync();
    }

    public Task<Customer> GetByCustomerWithLocationsEmployeesAsync(int id)
    {
        return DbContext.Customers
            .Include(x => x.Employees)
            .Include(x => x.BusinessLocations)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}