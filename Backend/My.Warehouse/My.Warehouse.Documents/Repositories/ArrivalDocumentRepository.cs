using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Dal.Entities.Documents.Arrival;
using My.Warehouse.Dal.Enums;
using My.Warehouse.Documents.Abstraction.Models.Arrival;
using My.Warehouse.Documents.Abstraction.Repositories;

namespace My.Warehouse.Documents.Repositories;

internal sealed class ArrivalDocumentRepository : IArrivalDocumentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ArrivalDocumentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ArrivalDocumentAddEditModel model)
    {
        var entity = new ArrivalDocumentEntity()
        {
            Id = Guid.NewGuid(),
            Number = model.Number,
            DocumentDate = model.DocumentDate,
            Status = model.Status,
            ArrivalResources = MapArrivalResource(model.Resources),
        };

        _dbContext.ArrivalDocument.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(ArrivalDocumentAddEditModel model)
    {
        ArrivalDocumentEntity entity = await _dbContext
            .ArrivalDocument.Include(x => x.ArrivalResources)
            .FirstAsync(x => x.Id == model.Id);

        entity.Number = model.Number;
        entity.DocumentDate = model.DocumentDate;
        entity.Status = model.Status;

        await _dbContext.SaveChangesAsync();

        await UpdateArrivalResources(model.Resources, model.Id.Value);
    }

    public async Task<ArrivalDocumentAddEditModel> GetById(Guid id)
    {
        return await _dbContext
            .ArrivalDocument.AsNoTracking()
            .Include(x => x.ArrivalResources)
            .Select(x => new ArrivalDocumentAddEditModel()
            {
                Id = x.Id,
                Number = x.Number,
                DocumentDate = x.DocumentDate,
                Status = x.Status,
                Resources = x.ArrivalResources.Select(y => new ArrivalResourceAddEditModel()
                {
                    Id = y.Id,
                    ResourceId = y.ResourceId,
                    MeasurementUnitId = y.MeasurementUnitId,
                    Amount = y.Amount,
                }),
            })
            .FirstAsync(x => x.Id == id);
    }

    public async Task<bool> CheckUnique(ArrivalDocumentAddEditModel model)
    {
        ArrivalDocumentEntity? entity = await _dbContext.ArrivalDocument.FirstOrDefaultAsync(x =>
            x.Id != model.Id && x.Status != DocumentStatus.Deleted && x.Number == model.Number
        );

        return entity == null;
    }

    public async Task<IEnumerable<ArrivalDocumentData>> GetAll(
        DateOnly? from,
        DateOnly? to,
        int? number,
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    )
    {
        var q = _dbContext
            .ArrivalDocument.Include(x => x.ArrivalResources)
            .ThenInclude(x => x.Resource)
            .Include(x => x.ArrivalResources)
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

        if (!resourceIds.IsNullOrEmpty())
        {
            q = q.Where(x => x.ArrivalResources.Any(y => resourceIds.Any(z => z == y.ResourceId)));
        }

        if (!measurementUnitIds.IsNullOrEmpty())
        {
            q = q.Where(x =>
                x.ArrivalResources.Any(y => measurementUnitIds.Any(z => z == y.MeasurementUnitId))
            );
        }

        return await q.OrderBy(x => x.Number)
            .Select(x => new ArrivalDocumentData()
            {
                Id = x.Id,
                Number = x.Number,
                DocumentDate = x.DocumentDate,
                Resources = x.ArrivalResources.Select(y => new ArrivalResourceData()
                {
                    Id = y.Id,
                    ResourceName = y.Resource.Name,
                    MeasurementUnitName = y.MeasurementUnit.Name,
                    Amount = y.Amount,
                }),
            })
            .ToArrayAsync();
    }

    private async Task UpdateArrivalResources(
        IEnumerable<ArrivalResourceAddEditModel> newResources,
        Guid documentId
    )
    {
        List<ArrivalResourceEntity> oldResources = await _dbContext
            .ArrivalResource.Where(x => x.ArrivalDocumentId == documentId)
            .ToListAsync();

        Dictionary<Guid, ArrivalResourceEntity> oldResourceDict = oldResources.ToDictionary(x =>
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

                _dbContext.ArrivalResource.Update(existing);
                oldResourceDict.Remove(existing.Id);
            }
            else
            {
                var newResource = new ArrivalResourceEntity
                {
                    Id = Guid.NewGuid(),
                    ResourceId = resource.ResourceId,
                    MeasurementUnitId = resource.MeasurementUnitId,
                    Amount = resource.Amount,
                    ArrivalDocumentId = documentId,
                };

                _dbContext.ArrivalResource.Add(newResource);
            }
        }

        foreach (var toRemove in oldResourceDict.Values)
        {
            _dbContext.ArrivalResource.Remove(toRemove);
        }

        await _dbContext.SaveChangesAsync();
    }

    private List<ArrivalResourceEntity> MapArrivalResource(
        IEnumerable<ArrivalResourceAddEditModel> models
    )
    {
        return models
            .Select(model => new ArrivalResourceEntity()
            {
                Id = model.Id ?? Guid.NewGuid(),
                ResourceId = model.ResourceId,
                MeasurementUnitId = model.MeasurementUnitId,
                Amount = model.Amount,
            })
            .ToList();
    }
}
