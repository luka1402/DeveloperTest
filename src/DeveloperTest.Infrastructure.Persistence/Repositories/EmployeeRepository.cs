using DeveloperTest.Core.Domain;
using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DeveloperTest.Infrastructure.Persistence.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DeveloperTestContext _ctx;

    public EmployeeRepository(DeveloperTestContext ctx)
    {
        _ctx = ctx;
    }

    public IUnitOfWork UnitOfWork => _ctx;

    public async Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
    {
        var entityEntry = await _ctx.Employees.AddAsync(employee, cancellationToken);
        return entityEntry.Entity;
    }

    public async Task<ICollection<Employee>> GetAllAsync()
    {
        return await _ctx.Employees.ToListAsync();
    }
   
}

