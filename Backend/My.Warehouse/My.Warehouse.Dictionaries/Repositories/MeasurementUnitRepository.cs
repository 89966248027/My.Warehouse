using Microsoft.EntityFrameworkCore;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Dal.Entities.Dictionaries;
using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.MeasurementUnits;
using My.Warehouse.Dictionaries.Abstraction.Repositories;

namespace My.Warehouse.Dictionaries.Repositories;

internal sealed class MeasurementUnitRepository : IMeasurementUnitRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MeasurementUnitRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(MeasurementUnitAddEditModel model)
    {
        var entity = new MeasurementUnitEntity()
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Status = model.Status,
        };

        _dbContext.MeasurementUnit.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(MeasurementUnitAddEditModel model)
    {
        MeasurementUnitEntity entity = await _dbContext.MeasurementUnit.FirstAsync(x =>
            x.Id == model.Id.Value
        );

        entity.Name = model.Name;
        entity.Status = model.Status;

        _dbContext.MeasurementUnit.Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<MeasurementUnitAddEditModel> GetById(Guid id)
    {
        return await _dbContext
            .MeasurementUnit.AsNoTracking()
            .Include(x => x.Balances)
            .Include(x => x.ShipmentResources)
            .Include(x => x.ArrivalResources)
            .Select(x => new MeasurementUnitAddEditModel()
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                HasDeleted =
                    (x.Balances == null || x.Balances.Count == 0)
                    && (x.ArrivalResources == null || x.ArrivalResources.Count == 0)
                    && (x.ShipmentResources == null || x.ShipmentResources.Count == 0),
            })
            .FirstAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<MeasurementUnitData>> GetAll(CommonStatus? status)
    {
        var q = _dbContext.MeasurementUnit.AsNoTracking();

        if (status != null)
        {
            q = q.Where(x => x.Status == status.Value);
        }

        return await q.OrderBy(x => x.Name)
            .Select(x => new MeasurementUnitData() { Id = x.Id, Name = x.Name })
            .ToArrayAsync();
    }

    public async Task<bool> CheckUnique(MeasurementUnitAddEditModel model)
    {
        MeasurementUnitEntity? entity = await _dbContext.MeasurementUnit.FirstOrDefaultAsync(x =>
            x.Id != model.Id && x.Status != CommonStatus.Deleted && x.Name == model.Name
        );

        return entity == null;
    }
}
