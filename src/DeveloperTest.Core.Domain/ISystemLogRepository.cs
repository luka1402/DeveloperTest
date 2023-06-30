using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;

namespace DeveloperTest.Core.Domain;

public interface ISystemLogRepository : IRepository
{
    Task<SystemLog> AddSystemLogAsync(SystemLog systemLog, CancellationToken cancellationToken);
}

