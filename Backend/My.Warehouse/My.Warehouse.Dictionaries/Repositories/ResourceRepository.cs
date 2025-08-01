using Microsoft.EntityFrameworkCore;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Dal.Entities.Dictionaries;
using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.Resources;
using My.Warehouse.Dictionaries.Abstraction.Repositories;

namespace My.Warehouse.Dictionaries.Repositories;

internal sealed class ResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ResourceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ResourceAddEditModel model)
    {
        var entity = new ResourceEntity()
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Status = model.Status,
        };

        _dbContext.Resource.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(ResourceAddEditModel model)
    {
        ResourceEntity entity = await _dbContext.Resource.FirstAsync(x => x.Id == model.Id.Value);

        entity.Name = model.Name;
        entity.Status = model.Status;

        _dbContext.Resource.Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ResourceAddEditModel> GetById(Guid id)
    {
        return await _dbContext
            .Resource.AsNoTracking()
            .Include(x => x.Balances)
            .Include(x => x.ShipmentResources)
            .Include(x => x.ArrivalResources)
            .Select(x => new ResourceAddEditModel()
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

    public async Task<IEnumerable<ResourceData>> GetAll(CommonStatus? status)
    {
        var q = _dbContext.Resource.AsNoTracking();

        if (status != null)
        {
            q = q.Where(x => x.Status == status.Value);
        }

        return await q.OrderBy(x => x.Name)
            .Select(x => new ResourceData() { Id = x.Id, Name = x.Name })
            .ToArrayAsync();
    }

    public async Task<bool> CheckUnique(ResourceAddEditModel model)
    {
        ResourceEntity? entity = await _dbContext.Resource.FirstOrDefaultAsync(x =>
            x.Id != model.Id && x.Status != CommonStatus.Deleted && x.Name == model.Name
        );

        return entity == null;
    }
}
