using DeveloperTest.Core.Domain;
using DeveloperTest.Core.Domain.Models;
using DeveloperTest.Core.Framework;

namespace DeveloperTest.Infrastructure.Persistence.Repositories;

public class SystemLogRepository : ISystemLogRepository
{
    private readonly DeveloperTestContext _ctx;

    public SystemLogRepository(DeveloperTestContext ctx)
    {
        _ctx = ctx;
    }

    public IUnitOfWork UnitOfWork => _ctx;

    public async Task<SystemLog> AddSystemLogAsync(SystemLog systemLog, CancellationToken cancellationToken)
    {
        var entityEntry = await _ctx.SystemLogs.AddAsync(systemLog, cancellationToken);
        return entityEntry.Entity;
    }
}

