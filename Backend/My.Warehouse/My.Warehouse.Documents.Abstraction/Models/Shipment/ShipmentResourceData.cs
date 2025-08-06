namespace My.Warehouse.Documents.Abstraction.Models.Shipment;

public sealed record ShipmentResourceData
{
    public Guid Id { get; init; }

    public string ResourceName { get; init; }

    public string MeasurementUnitName { get; init; }

    public decimal Amount { get; init; }
}
