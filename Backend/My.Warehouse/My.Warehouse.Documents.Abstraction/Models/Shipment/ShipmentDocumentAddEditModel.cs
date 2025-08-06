using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Documents.Abstraction.Models.Shipment;

public sealed record ShipmentDocumentAddEditModel
{
    public Guid? Id { get; init; }

    public int Number { get; init; }

    public Guid ClientId { get; init; }

    public DateOnly DocumentDate { get; init; }

    public DocumentStatus Status { get; init; }

    public IEnumerable<ShipmentResourceAddEditModel> Resources { get; init; }
}
