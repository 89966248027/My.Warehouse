namespace My.Warehouse.Documents.Abstraction.Models.Shipment;

public sealed record ShipmentResourceAddEditModel
{
    public Guid? Id { get; init; }

    public Guid ResourceId { get; init; }

    public Guid MeasurementUnitId { get; init; }

    public decimal Amount { get; init; }
}
