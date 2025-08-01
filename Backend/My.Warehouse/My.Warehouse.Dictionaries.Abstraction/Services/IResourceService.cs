using My.Warehouse.Dictionaries.Abstraction.Models.Resources;

namespace My.Warehouse.Dictionaries.Abstraction.Services;

public interface IResourceService
{
    Task Save(ResourceAddEditModel model);

    Task<ResourceAddEditModel> Get(Guid id);

    Task Delete(Guid id);

    Task Archive(Guid id);

    Task Active(Guid id);

    Task<IEnumerable<ResourceData>> GetAllActive();

    Task<IEnumerable<ResourceData>> GetAllArchived();

    Task<bool> CheckUnique(ResourceAddEditModel model);
}
