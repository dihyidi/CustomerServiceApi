namespace CustomerService.Application.Models;

public class BusinessLocationModel : Model
{
    public string Name { get; set; }

    public string Address { get; set; }

    public string PhoneNumber { get; set; }

    public int CustomerId { get; set; }

    public IList<int> EmployeesIds { get; set; }
}