namespace My.Warehouse.Documents.Abstraction.Models.Shipment;

public sealed record ShipmentDocumentData
{
    public Guid Id { get; init; }

    public int Number { get; init; }

    public string ClientName { get; init; }

    public DateOnly DocumentDate { get; init; }

    public IEnumerable<ShipmentResourceData> Resources { get; init; }
}
