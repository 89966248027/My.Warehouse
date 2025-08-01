using Microsoft.EntityFrameworkCore;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Dal.Entities.Dictionaries;
using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.Clients;
using My.Warehouse.Dictionaries.Abstraction.Repositories;

namespace My.Warehouse.Dictionaries.Repositories;

internal sealed class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ClientRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(ClientAddEditModel model)
    {
        var entity = new ClientEntity()
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Address = model.Address,
            Status = model.Status,
        };

        _dbContext.Client.Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(ClientAddEditModel model)
    {
        ClientEntity entity = await _dbContext.Client.FirstAsync(x => x.Id == model.Id.Value);

        entity.Name = model.Name;
        entity.Status = model.Status;
        entity.Address = model.Address;

        _dbContext.Client.Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ClientAddEditModel> GetById(Guid id)
    {
        return await _dbContext
            .Client.AsNoTracking()
            .Include(x => x.ShipmentDocuments)
            .Select(x => new ClientAddEditModel()
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                Address = x.Address,
                HasDeleted = x.ShipmentDocuments == null || x.ShipmentDocuments.Count == 0,
            })
            .FirstAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<ClientData>> GetAll(CommonStatus? status)
    {
        var q = _dbContext.Client.AsNoTracking();

        if (status != null)
        {
            q = q.Where(x => x.Status == status.Value);
        }

        return await q.OrderBy(x => x.Name)
            .Select(x => new ClientData()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
            })
            .ToArrayAsync();
    }

    public async Task<bool> CheckUnique(ClientAddEditModel model)
    {
        ClientEntity? entity = await _dbContext.Client.FirstOrDefaultAsync(x =>
            x.Id != model.Id && x.Status != CommonStatus.Deleted && x.Name == model.Name
        );

        return entity == null;
    }
}
