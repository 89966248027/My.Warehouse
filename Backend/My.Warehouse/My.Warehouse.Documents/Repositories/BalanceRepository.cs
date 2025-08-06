using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Dal.Entities.Dictionaries;
using My.Warehouse.Dal.Entities.Documents.Arrival;
using My.Warehouse.Dal.Entities.Documents.Shipment;
using My.Warehouse.Dal.Enums;
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

    public async Task Add(BalanceAddEditModel balance)
    {
        var entity = new BalanceEntity()
        {
            ResourceId = balance.ResourceId,
            MeasurementUnitId = balance.MeasurementUnitId,
            Amount = balance.Amount,
        };

        _db.Balance.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Update(BalanceAddEditModel balance)
    {
        BalanceEntity? entity = await _db.Balance.FirstAsync(x =>
            x.ResourceId == balance.ResourceId && x.MeasurementUnitId == balance.MeasurementUnitId
        );

        entity.Amount = balance.Amount;

        _db.Balance.Update(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Dictionary<Guid, Dictionary<Guid, decimal>>> GetResourceFundsLeft()
    {
        List<BalanceEntity> balance = await _db
            .Balance.Include(x => x.Resource)
            .Include(x => x.MeasurementUnit)
            .AsNoTracking()
            .ToListAsync();

        return balance
            .GroupBy(x => x.ResourceId)
            .ToDictionary(x => x.Key, x => x.ToDictionary(y => y.MeasurementUnitId, y => y.Amount));
    }

    public async Task<IEnumerable<BalanceFundsLeft>> GetDocumentAmounts(Guid documentId)
    {
        List<ArrivalResourceEntity> arrivalResources = await _db
            .ArrivalResource.AsNoTracking()
            .Where(x => x.ArrivalDocumentId == documentId)
            .ToListAsync();

        if (arrivalResources.Any())
        {
            return arrivalResources
                .Select(x => new BalanceFundsLeft()
                {
                    ResourceId = x.ResourceId,
                    MeasurementUnitId = x.MeasurementUnitId,
                    Amount = x.Amount,
                    DocumentType = DocumentType.Arrival,
                })
                .ToArray();
        }

        List<ShipmentResourceEntity> shipmentResources = await _db
            .ShipmentResource.AsNoTracking()
            .Include(x => x.ShipmentDocument)
            .Where(x => x.ShipmentDocument.Status == DocumentStatus.Signed)
            .Where(x => x.ShipmentDocumentId == documentId)
            .ToListAsync();

        if (shipmentResources.Any())
        {
            return shipmentResources
                .Select(x => new BalanceFundsLeft()
                {
                    ResourceId = x.ResourceId,
                    MeasurementUnitId = x.MeasurementUnitId,
                    Amount = x.Amount,
                    DocumentType = DocumentType.Shipment,
                })
                .ToArray();
        }

        return [];
    }

    public async Task<IEnumerable<BalanceFundsLeft>> GetDocumentAmounts()
    {
        IEnumerable<BalanceFundsLeft> arrivalResources = await _db
            .ArrivalResource.AsNoTracking()
            .Select(x => new BalanceFundsLeft()
            {
                ResourceId = x.ResourceId,
                MeasurementUnitId = x.MeasurementUnitId,
                Amount = x.Amount,
                DocumentType = DocumentType.Arrival,
            })
            .ToArrayAsync();

        IEnumerable<BalanceFundsLeft> shipmentResources = await _db
            .ShipmentResource.AsNoTracking()
            .Include(x => x.ShipmentDocument)
            .Where(x => x.ShipmentDocument.Status == DocumentStatus.Signed)
            .Select(x => new BalanceFundsLeft()
            {
                ResourceId = x.ResourceId,
                MeasurementUnitId = x.MeasurementUnitId,
                Amount = x.Amount,
                DocumentType = DocumentType.Shipment,
            })
            .ToListAsync();

        return arrivalResources.Concat(shipmentResources);
    }
}
