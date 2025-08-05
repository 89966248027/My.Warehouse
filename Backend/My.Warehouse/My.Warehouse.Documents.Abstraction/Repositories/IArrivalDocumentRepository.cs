using My.Warehouse.Documents.Abstraction.Models.Arrival;

namespace My.Warehouse.Documents.Abstraction.Repositories;

public interface IArrivalDocumentRepository
{
    Task Add(ArrivalDocumentAddEditModel model);

    Task Update(ArrivalDocumentAddEditModel model);

    Task<ArrivalDocumentAddEditModel> GetById(Guid id);

    Task<bool> CheckUnique(ArrivalDocumentAddEditModel model);

    Task<IEnumerable<ArrivalDocumentData>> GetAll(
        DateOnly? from,
        DateOnly? to,
        int? number,
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    );
}
