using CustomerService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerService.Infrastructure.EntityConfigurations;

public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> employeeConfiguration)
    {
        employeeConfiguration.HasKey(x => x.Id);

        employeeConfiguration.Property(x => x.FirstName).IsRequired();
        employeeConfiguration.Property(x => x.LastName).IsRequired();
        employeeConfiguration.Property(x => x.Email).IsRequired();
        employeeConfiguration.Property(x => x.PhoneNumber).IsRequired();

        employeeConfiguration
            .HasOne(x => x.Customer)
            .WithMany(x => x.Employees);

        employeeConfiguration
            .HasMany(x => x.BusinessLocations)
            .WithMany(x => x.Employees);
    }
}