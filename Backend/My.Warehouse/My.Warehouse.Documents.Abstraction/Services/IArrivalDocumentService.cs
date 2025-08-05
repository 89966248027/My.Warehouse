using My.Warehouse.Documents.Abstraction.Models.Arrival;
using My.Warehouse.Documents.Abstraction.Models.Balance;

namespace My.Warehouse.Documents.Abstraction.Services;

public interface IArrivalDocumentService
{
    Task Save(ArrivalDocumentAddEditModel model);

    Task<ArrivalDocumentAddEditModel> GetById(Guid id);

    Task Delete(Guid id);

    Task<bool> CheckUnique(ArrivalDocumentAddEditModel model);

    Task<IEnumerable<ArrivalDocumentData>> GetAll(
        DateOnly? from,
        DateOnly? to,
        int? number,
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    );
}
