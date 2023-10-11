namespace CustomerService.Domain.Models;

public class Employee : Entity
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual List<BusinessLocation> BusinessLocations { get; set; }
}