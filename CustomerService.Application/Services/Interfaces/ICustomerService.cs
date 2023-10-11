using CustomerService.Application.Models;
using CustomerService.Application.Models.Create;

namespace CustomerService.Application.Services.Interfaces;

public interface ICustomerService
{
    Task<IList<CustomerModel>> GetAllCustomers();

    Task<CustomerModel> GetCustomerById(int id);

    Task<CustomerModel> GetCustomerByNumber(string customerNumber);

    Task<int> CreateCustomer(PureCustomerModel pureCustomer);

    Task UpdateCustomer(int id, PureCustomerModel pureCustomer);

    Task DeleteCustomer(int id);

    Task<IList<BusinessLocationModel>> GetAllBusinessLocationsByCustomerId(int customerId);

    Task AddBusinessLocationToCustomer(int customerId, PureBusinessLocationModel location);

    Task RemoveBusinessLocationFromCustomer(int customerId, int locationId);

    Task<IList<EmployeeModel>> GetAllEmployeesByCustomerId(int customerId);

    Task AddEmployeeToCustomer(int customerId, PureEmployeeModel pureEmployee);

    Task RemoveEmployeeFromCustomer(int customerId, int employeeId);
}