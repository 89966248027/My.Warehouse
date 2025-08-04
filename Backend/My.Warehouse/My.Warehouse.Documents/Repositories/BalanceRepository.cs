using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Documents.Abstraction.Models.Balance;
using My.Warehouse.Documents.Abstraction.Repositories;

namespace My.Warehouse.Documents.Repositories;

internal sealed class BalanceRepository : IBalanceRepository
{
    private readonly ApplicationDbContext _db;

    public BalanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<BalanceData>> GetAll(
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    )
    {
        var q = _db.Balance.Include(x => x.Resource).Include(x => x.MeasurementUnit).AsNoTracking();

        if (!resourceIds.IsNullOrEmpty())
        {
            q = q.Where(x => resourceIds.Any(y => y == x.ResourceId));
        }

        if (!measurementUnitIds.IsNullOrEmpty())
        {
            q = q.Where(x => measurementUnitIds.Any(y => y == x.MeasurementUnitId));
        }

        return await q.Select(x => new BalanceData()
            {
                Id = x.Id,
                ResourceName = x.Resource.Name,
                MeasurementUnitName = x.MeasurementUnit.Name,
                Amount = x.Amount,
            })
            .ToArrayAsync();
    }
}
