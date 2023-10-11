using CustomerService.Application.Mapping;
using CustomerService.Application.Models;
using CustomerService.Application.Models.Create;
using CustomerService.Application.Services.Interfaces;
using CustomerService.Domain.Repositories;

namespace CustomerService.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IBusinessLocationRepository businessLocationRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly IEmployeeRepository employeeRepository;

    public CustomerService(ICustomerRepository customerRepository,
        IBusinessLocationRepository businessLocationRepository,
        IEmployeeRepository employeeRepository)
    {
        this.customerRepository = customerRepository;
        this.businessLocationRepository = businessLocationRepository;
        this.employeeRepository = employeeRepository;
    }

    public async Task<IList<CustomerModel>> GetAllCustomers()
    {
        var all = await customerRepository.GetByCustomersWithLocationsEmployeesAsync();
        return all.Select(Mapper.MapCustomerToModel).ToList();
    }

    public async Task<CustomerModel> GetCustomerById(int id)
    {
        var customer = await customerRepository.GetByCustomerWithLocationsEmployeesAsync(id);
        return customer is not null ? Mapper.MapCustomerToModel(customer) : null;
    }

    public async Task<CustomerModel> GetCustomerByNumber(string customerNumber)
    {
        var customer = await customerRepository.GetByCustomerNumberAsync(customerNumber);
        return customer is not null ? Mapper.MapCustomerToModel(customer) : null;
    }

    public async Task<int> CreateCustomer(PureCustomerModel pureCustomer)
    {
        var entity = Mapper.MapCustomerModelToEntity(pureCustomer);
        var result = await customerRepository.CreateAsync(entity);
        return result.Id;
    }

    public async Task UpdateCustomer(int id, PureCustomerModel pureCustomer)
    {
        var customerToUpdate = await customerRepository.GetByIdAsync(id);
        if (customerToUpdate is null) return;

        customerToUpdate.CustomerNumber = pureCustomer.CustomerNumber;
        customerToUpdate.Name = pureCustomer.Name;

        await customerRepository.UpdateAsync(customerToUpdate);
    }

    public async Task DeleteCustomer(int id)
    {
        var customerToDelete = await customerRepository.GetByIdAsync(id);
        if (customerToDelete is null) return;

        await customerRepository.Delete(customerToDelete);
    }

    public async Task<IList<BusinessLocationModel>> GetAllBusinessLocationsByCustomerId(int customerId)
    {
        return (await businessLocationRepository.GetLocationsByCustomerId(customerId))
            .Select(Mapper.MapBusinessLocationToModel).ToList();
    }

    public async Task AddBusinessLocationToCustomer(int customerId, PureBusinessLocationModel location)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);
        if (customer is null) return;

        customer.BusinessLocations.Add(Mapper.MapBusinessLocationModelToEntity(location));
        await customerRepository.UpdateAsync(customer);
    }

    public async Task RemoveBusinessLocationFromCustomer(int customerId, int locationId)
    {
        var customer = await customerRepository.GetByIdAsync(customerId);
        if (customer is null) return;

        customer.BusinessLocations = customer.BusinessLocations.Where(x => x.Id != locationId).ToList();
        await customerRepository.UpdateAsync(customer);
    }

    public async Task<IList<EmployeeModel>> GetAllEmployeesByCustomerId(int customerId)
    {
        return (await employeeRepository.GetEmployeesByCustomerId(customerId))
            .Select(Mapper.MapEmployeeToModel).ToList();
    }

    public async Task AddEmployeeToCustomer(int customerId, PureEmployeeModel pureEmployee)
    {
        var customer = await customerRepository.GetByCustomerWithLocationsEmployeesAsync(customerId);
        if (customer is null) return;

        customer.Employees.Add(Mapper.MapEmployeeModelToEntity(pureEmployee));
        await customerRepository.UpdateAsync(customer);
    }

    public async Task RemoveEmployeeFromCustomer(int customerId, int employeeId)
    {
        var customer = await customerRepository.GetByCustomerWithLocationsEmployeesAsync(customerId);
        if (customer is null) return;

        customer.Employees = customer.Employees.Where(x => x.Id != employeeId).ToList();
        await customerRepository.UpdateAsync(customer);
    }
}