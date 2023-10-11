namespace CustomerService.Domain.Models;

public class Customer : Entity
{
    public string CustomerNumber { get; set; }

    public string Name { get; set; }

    public virtual List<Employee> Employees { get; set; }

    public virtual List<BusinessLocation> BusinessLocations { get; set; }
}