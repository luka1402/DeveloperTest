using DeveloperTest.Core.Domain;
using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;

namespace DeveloperTest.Infrastructure.Persistence.Repositories;

public class EmployeeCompanyRepository : IEmployeeCompanyRepository
{
    private readonly DeveloperTestContext _ctx;

    public EmployeeCompanyRepository(DeveloperTestContext ctx)
    {
        _ctx = ctx;
    }
    public IUnitOfWork UnitOfWork => _ctx;

    public async Task<EmployeeCompany> AddEmployeeCompanyAsync(EmployeeCompany employeeCompany, CancellationToken cancellationToken)
    {
        var entityEntry = await _ctx.EmployeeCompanies.AddAsync(employeeCompany, cancellationToken);
        return entityEntry.Entity;
    }
}

