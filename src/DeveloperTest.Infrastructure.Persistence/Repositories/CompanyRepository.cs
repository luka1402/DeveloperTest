using DeveloperTest.Core.Domain;
using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Infrastructure.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DeveloperTestContext _ctx;

    public CompanyRepository(DeveloperTestContext ctx)
    {
        _ctx = ctx;
    }
    public IUnitOfWork UnitOfWork => _ctx;

    public async Task<Company> AddCompanyAsync(Company company, CancellationToken cancellationToken)
    {
        var entityEntry = await _ctx.Companies.AddAsync(company, cancellationToken);
        return entityEntry.Entity;
    }

    public async Task<ICollection<Company>> GetAllAsync()
    {
        return await _ctx.Companies.ToListAsync();
    }
}

