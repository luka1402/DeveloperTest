using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;

namespace DeveloperTest.Core.Domain;

public interface ICompanyRepository : IRepository
{
    Task<ICollection<Company>> GetAllAsync();
    Task<Company> AddCompanyAsync(Company company, CancellationToken cancellationToken);
}

