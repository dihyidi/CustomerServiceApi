namespace CustomerService.Application.Models;

public class CustomerModel
{
    public int Id { get; set; }
    public string CustomerNumber { get; set; }

    public string Name { get; set; }

    public virtual IEnumerable<EmployeeModel> Employees { get; set; }

    public virtual IEnumerable<BusinessLocationModel> BusinessLocations { get; set; }
}