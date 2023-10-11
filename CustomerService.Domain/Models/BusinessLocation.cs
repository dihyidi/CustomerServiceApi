namespace CustomerService.Domain.Models;

public class BusinessLocation : Entity
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual List<Employee> Employees { get; set; }
}