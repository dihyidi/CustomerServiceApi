using CustomerService.Domain.Models;
using CustomerService.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Infrastructure;

public sealed class CustomerServiceDbContext : DbContext
{
    public CustomerServiceDbContext(DbContextOptions<CustomerServiceDbContext> options) : base(options)
    {
        // for testing
        Database.EnsureCreated();
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<BusinessLocation> BusinessLocations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BusinessLocationEntityTypeConfiguration());
    }
}