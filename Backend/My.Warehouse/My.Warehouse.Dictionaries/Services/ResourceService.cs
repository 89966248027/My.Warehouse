using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.Resources;
using My.Warehouse.Dictionaries.Abstraction.Repositories;
using My.Warehouse.Dictionaries.Abstraction.Services;

namespace My.Warehouse.Dictionaries.Services;

internal sealed class ResourceService : IResourceService
{
    private readonly IResourceRepository _repository;

    public ResourceService(IResourceRepository repository)
    {
        _repository = repository;
    }

    public async Task Save(ResourceAddEditModel model)
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

    public async Task<ResourceAddEditModel> Get(Guid id)
    {
        return await _repository.GetById(id);
    }

    public async Task Delete(Guid id)
    {
        ResourceAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Deleted });
    }

    public async Task Archive(Guid id)
    {
        ResourceAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Archived });
    }

    public async Task Active(Guid id)
    {
        ResourceAddEditModel model = await _repository.GetById(id);

        await _repository.Update(model with { Status = CommonStatus.Active });
    }

    public async Task<IEnumerable<ResourceData>> GetAllActive()
    {
        return await _repository.GetAll(CommonStatus.Active);
    }

    public async Task<IEnumerable<ResourceData>> GetAllArchived()
    {
        return await _repository.GetAll(CommonStatus.Archived);
    }

    public async Task<bool> CheckUnique(ResourceAddEditModel model)
    {
        return await _repository.CheckUnique(model);
    }
}
