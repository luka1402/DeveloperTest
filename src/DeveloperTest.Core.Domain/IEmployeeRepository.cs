
using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;

namespace DeveloperTest.Core.Domain;

public interface IEmployeeRepository : IRepository
{
   Task<ICollection<Employee>> GetAllAsync();
   Task<Employee> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
}

