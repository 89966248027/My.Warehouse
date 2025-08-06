using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Dal.Entities.Documents.Shipment;
using My.Warehouse.Dal.Enums;
using My.Warehouse.Documents.Abstraction.Models.Shipment;
using My.Warehouse.Documents.Abstraction.Repositories;

namespace My.Warehouse.Documents.Repositories;

internal sealed class ShipmentDocumentRepository : IShipmentDocumentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ShipmentDocumentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ShipmentDocumentAddEditModel model)
    {
        var entity = new ShipmentDocumentEntity()
        {
            Id = Guid.NewGuid(),
            Number = model.Number,
            ClientId = model.ClientId,
            DocumentDate = model.DocumentDate,
            Status = model.Status,
            ShipmentResources = MapShipmentResource(model.Resources),
        };

        _dbContext.ShipmentDocument.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(ShipmentDocumentAddEditModel model)
    {
        ShipmentDocumentEntity entity = await _dbContext
            .ShipmentDocument.Include(x => x.ShipmentResources)
            .FirstAsync(x => x.Id == model.Id);

        entity.Number = model.Number;
        entity.DocumentDate = model.DocumentDate;
        entity.ClientId = model.ClientId;
        entity.Status = model.Status;

        await _dbContext.SaveChangesAsync();

        await UpdateShipmentResources(model.Resources, model.Id.Value);
    }

    public async Task<ShipmentDocumentAddEditModel> GetById(Guid id)
    {
        return await _dbContext
            .ShipmentDocument.AsNoTracking()
            .Include(x => x.ShipmentResources)
            .Select(x => new ShipmentDocumentAddEditModel()
            {
                Id = x.Id,
                Number = x.Number,
                ClientId = x.ClientId,
                DocumentDate = x.DocumentDate,
                Status = x.Status,
                Resources = x.ShipmentResources.Select(y => new ShipmentResourceAddEditModel()
                {
                    Id = y.Id,
                    ResourceId = y.ResourceId,
                    MeasurementUnitId = y.MeasurementUnitId,
                    Amount = y.Amount,
                }),
            })
            .FirstAsync(x => x.Id == id);
    }

    public async Task<bool> CheckUnique(ShipmentDocumentAddEditModel model)
    {
        ShipmentDocumentEntity? entity = await _dbContext.ShipmentDocument.FirstOrDefaultAsync(x =>
            x.Id != model.Id && x.Status != DocumentStatus.Deleted && x.Number == model.Number
        );

        return entity == null;
    }

    public async Task<IEnumerable<ShipmentDocumentData>> GetAll(
        DateOnly? from,
        DateOnly? to,
        IEnumerable<Guid> clientIds,
        int? number,
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    )
    {
        var q = _dbContext
            .ShipmentDocument.Include(x => x.Client)
            .Include(x => x.ShipmentResources)
            .ThenInclude(x => x.Resource)
            .Include(x => x.ShipmentResources)
            .ThenInclude(x => x.MeasurementUnit)
            .Where(x => x.Status != DocumentStatus.Deleted)
            .AsNoTracking();

        if (from.HasValue)
        {
            q = q.Where(x => x.DocumentDate >= from);
        }

        if (to.HasValue)
        {
            q = q.Where(x => x.DocumentDate <= to);
        }

        if (number.HasValue)
        {
            q = q.Where(x => x.Number.ToString().StartsWith(number.Value.ToString()));
        }

        if (!clientIds.IsNullOrEmpty())
        {
            q = q.Where(x => clientIds.Any(y => y == x.Id));
        }

        if (!resourceIds.IsNullOrEmpty())
        {
            q = q.Where(x => x.ShipmentResources.Any(y => resourceIds.Any(z => z == y.ResourceId)));
        }

        if (!measurementUnitIds.IsNullOrEmpty())
        {
            q = q.Where(x =>
                x.ShipmentResources.Any(y => measurementUnitIds.Any(z => z == y.MeasurementUnitId))
            );
        }

        return await q.OrderBy(x => x.Number)
            .Select(x => new ShipmentDocumentData()
            {
                Id = x.Id,
                Number = x.Number,
                ClientName = x.Client.Name,
                DocumentDate = x.DocumentDate,
                Resources = x.ShipmentResources.Select(y => new ShipmentResourceData()
                {
                    Id = y.Id,
                    ResourceName = y.Resource.Name,
                    MeasurementUnitName = y.MeasurementUnit.Name,
                    Amount = y.Amount,
                }),
            })
            .ToArrayAsync();
    }

    private async Task UpdateShipmentResources(
        IEnumerable<ShipmentResourceAddEditModel> newResources,
        Guid documentId
    )
    {
        List<ShipmentResourceEntity> oldResources = await _dbContext
            .ShipmentResource.Where(x => x.ShipmentDocumentId == documentId)
            .ToListAsync();

        Dictionary<Guid, ShipmentResourceEntity> oldResourceDict = oldResources.ToDictionary(x =>
            x.Id
        );

        foreach (var resource in newResources)
        {
            if (
                resource.Id is not null
                && oldResourceDict.TryGetValue(resource.Id.Value, out var existing)
            )
            {
                existing.ResourceId = resource.ResourceId;
                existing.MeasurementUnitId = resource.MeasurementUnitId;
                existing.Amount = resource.Amount;

                _dbContext.ShipmentResource.Update(existing);
                oldResourceDict.Remove(existing.Id);
            }
            else
            {
                var newResource = new ShipmentResourceEntity
                {
                    Id = Guid.NewGuid(),
                    ResourceId = resource.ResourceId,
                    MeasurementUnitId = resource.MeasurementUnitId,
                    Amount = resource.Amount,
                    ShipmentDocumentId = documentId,
                };

                _dbContext.ShipmentResource.Add(newResource);
            }
        }

        foreach (var toRemove in oldResourceDict.Values)
        {
            _dbContext.ShipmentResource.Remove(toRemove);
        }

        await _dbContext.SaveChangesAsync();
    }

    private List<ShipmentResourceEntity> MapShipmentResource(
        IEnumerable<ShipmentResourceAddEditModel> models
    )
    {
        return models
            .Select(model => new ShipmentResourceEntity()
            {
                Id = model.Id ?? Guid.NewGuid(),
                ResourceId = model.ResourceId,
                MeasurementUnitId = model.MeasurementUnitId,
                Amount = model.Amount,
            })
            .ToList();
    }
}
