using DeveloperTest.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Core.Application.Interfaces;

public interface IDeveloperTestContext
{
    DbSet<Employee> Employees { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<EmployeeCompany> EmployeeCompanies { get; set; }
    DbSet<SystemLog> SystemLogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}

