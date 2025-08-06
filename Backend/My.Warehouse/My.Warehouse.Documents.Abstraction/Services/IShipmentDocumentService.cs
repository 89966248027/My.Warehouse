using My.Warehouse.Documents.Abstraction.Models.Shipment;

namespace My.Warehouse.Documents.Abstraction.Services;

public interface IShipmentDocumentService
{
    Task Save(ShipmentDocumentAddEditModel model);

    Task<ShipmentDocumentAddEditModel> GetById(Guid id);

    Task Delete(Guid id);

    Task Revoke(Guid id);

    Task<bool> CheckUnique(ShipmentDocumentAddEditModel model);

    Task<IEnumerable<ShipmentDocumentData>> GetAll(
        DateOnly? from,
        DateOnly? to,
        IEnumerable<Guid> clientIds,
        int? number,
        IEnumerable<Guid> resourceIds,
        IEnumerable<Guid> measurementUnitIds
    );
}
