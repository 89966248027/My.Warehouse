using My.Warehouse.Dal.Enums;
using My.Warehouse.Dictionaries.Abstraction.Models.Clients;

namespace My.Warehouse.Dictionaries.Abstraction.Repositories;

public interface IClientRepository
{
    Task Add(ClientAddEditModel model);

    Task Update(ClientAddEditModel model);

    Task<ClientAddEditModel> GetById(Guid id);

    Task<IEnumerable<ClientData>> GetAll(CommonStatus? status);

    Task<bool> CheckUnique(ClientAddEditModel model);
}
