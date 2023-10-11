using CustomerService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerService.Infrastructure.EntityConfigurations;

public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> customerConfiguration)
    {
        customerConfiguration.HasKey(x => x.Id);

        customerConfiguration.HasIndex(x => x.CustomerNumber).IsUnique();

        customerConfiguration.Property(x => x.Name).IsRequired();

        customerConfiguration
            .HasMany(x => x.Employees)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);

        customerConfiguration
            .HasMany(x => x.BusinessLocations)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}