namespace CustomerService.Application.Models;

public class EmployeeModel : Model
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public int CustomerId { get; set; }

    public IList<int> BusinessLocationsIds { get; set; }
}