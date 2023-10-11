using CustomerService.Application.Models;
using CustomerService.Application.Models.Create;
using CustomerService.Domain.Models;

namespace CustomerService.Application.Mapping;

public static class Mapper
{
    public static CustomerModel MapCustomerToModel(Customer customer)
    {
        return new CustomerModel
        {
            Id = customer.Id,
            CustomerNumber = customer.CustomerNumber,
            Name = customer.Name,
            Employees = customer.Employees.Select(MapEmployeeToModel),
            BusinessLocations = customer.BusinessLocations.Select(MapBusinessLocationToModel)
        };
    }

    public static Customer MapCustomerModelToEntity(PureCustomerModel pureCustomer)
    {
        return new Customer
        {
            CustomerNumber = pureCustomer.CustomerNumber,
            Name = pureCustomer.Name
        };
    }

    public static EmployeeModel MapEmployeeToModel(Employee employee)
    {
        return new EmployeeModel
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber
        };
    }

    public static Employee MapEmployeeModelToEntity(PureEmployeeModel pureEmployee)
    {
        return new Employee
        {
            FirstName = pureEmployee.FirstName,
            LastName = pureEmployee.LastName,
            Email = pureEmployee.Email,
            PhoneNumber = pureEmployee.PhoneNumber
        };
    }

    public static BusinessLocationModel MapBusinessLocationToModel(BusinessLocation businessLocation)
    {
        return new BusinessLocationModel
        {
            Id = businessLocation.Id,
            Address = businessLocation.Address,
            Name = businessLocation.Name,
            PhoneNumber = businessLocation.PhoneNumber
        };
    }

    public static BusinessLocation MapBusinessLocationModelToEntity(PureBusinessLocationModel pureBusinessLocation)
    {
        return new BusinessLocation
        {
            Address = pureBusinessLocation.Address,
            Name = pureBusinessLocation.Name,
            PhoneNumber = pureBusinessLocation.PhoneNumber
        };
    }
}