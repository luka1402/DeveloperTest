using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;

namespace DeveloperTest.Core.Domain;

public interface IEmployeeCompanyRepository :IRepository
{
    Task<EmployeeCompany> AddEmployeeCompanyAsync(EmployeeCompany employeeCompany, CancellationToken cancellationToken);
}

