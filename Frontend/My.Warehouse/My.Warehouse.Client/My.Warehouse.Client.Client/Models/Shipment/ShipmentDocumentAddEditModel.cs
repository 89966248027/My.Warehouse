using My.Warehouse.Client.Client.Enums;

namespace My.Warehouse.Client.Client.Models.Shipment;

public sealed class ShipmentDocumentAddEditModel
{
    public Guid? Id { get; set; }

    public int? Number { get; set; }

    public Guid? ClientId { get; set; }

    public DateOnly DocumentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public DocumentStatus Status { get; set; } = DocumentStatus.Created;

    public List<ShipmentResourceAddEditModel> Resources { get; set; } = [];
}
