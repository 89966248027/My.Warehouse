using My.Warehouse.Documents.Abstraction.Models.Shipment;

namespace My.Warehouse.Documents.Abstraction.Repositories;

public interface IShipmentDocumentRepository
{
    Task Add(ShipmentDocumentAddEditModel model);

    Task Update(ShipmentDocumentAddEditModel model);

    Task<ShipmentDocumentAddEditModel> GetById(Guid id);

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
