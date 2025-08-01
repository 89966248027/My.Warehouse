using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.Resources;

namespace My.Warehouse.Dictionaries.Abstraction.Repositories;

public interface IResourceRepository
{
    Task Add(ResourceAddEditModel model);

    Task Update(ResourceAddEditModel model);

    Task<ResourceAddEditModel> GetById(Guid id);

    Task<IEnumerable<ResourceData>> GetAll(CommonStatus? status);

    Task<bool> CheckUnique(ResourceAddEditModel model);
}
