using My.Warehouse.Dictionaries.Abstraction.Models.Clients;

namespace My.Warehouse.Dictionaries.Abstraction.Services;

public interface IClientService
{
    Task Save(ClientAddEditModel model);

    Task<ClientAddEditModel> Get(Guid id);

    Task Delete(Guid id);

    Task Archive(Guid id);

    Task Active(Guid id);

    Task<IEnumerable<ClientData>> GetAllActive();

    Task<IEnumerable<ClientData>> GetAllArchived();

    Task<bool> CheckUnique(ClientAddEditModel model);
}
