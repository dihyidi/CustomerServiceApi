using CustomerService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerService.Infrastructure.EntityConfigurations;

public class BusinessLocationEntityTypeConfiguration : IEntityTypeConfiguration<BusinessLocation>
{
    public void Configure(EntityTypeBuilder<BusinessLocation> businessLocationConfiguration)
    {
        businessLocationConfiguration.HasKey(x => x.Id);

        businessLocationConfiguration.Property(x => x.Name).IsRequired();
        businessLocationConfiguration.Property(x => x.Address).IsRequired();
        businessLocationConfiguration.Property(x => x.PhoneNumber).IsRequired();

        businessLocationConfiguration
            .HasOne(x => x.Customer)
            .WithMany(x => x.BusinessLocations);

        businessLocationConfiguration
            .HasMany(x => x.Employees)
            .WithMany(x => x.BusinessLocations);
    }
}