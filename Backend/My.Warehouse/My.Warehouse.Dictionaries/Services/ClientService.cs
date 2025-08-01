using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.Clients;
using My.Warehouse.Dictionaries.Abstraction.Repositories;
using My.Warehouse.Dictionaries.Abstraction.Services;

namespace My.Warehouse.Dictionaries.Services;

internal sealed class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task Save(ClientAddEditModel model)
    {
        if (model.Id == null)
        {
            await _repository.Add(model with { Status = CommonStatus.Active });
        }
        else
        {
            await _repository.Update(model with { Status = CommonStatus.Active });
        }
    }

    public async Task<ClientAddEditModel> Get(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Delete(Guid id)
    {
        ClientAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Deleted });
    }

    public async Task Archive(Guid id)
    {
        ClientAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Archived });
    }

    public async Task Active(Guid id)
    {
        ClientAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Active });
    }

    public async Task<IEnumerable<ClientData>> GetAllActive()
    {
        return await _repository.GetAll(CommonStatus.Active);
    }

    public async Task<IEnumerable<ClientData>> GetAllArchived()
    {
        return await _repository.GetAll(CommonStatus.Archived);
    }

    public async Task<bool> CheckUnique(ClientAddEditModel model)
    {
        return await _repository.CheckUnique(model);
    }
}
