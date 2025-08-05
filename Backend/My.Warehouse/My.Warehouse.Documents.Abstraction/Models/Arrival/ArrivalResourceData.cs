namespace My.Warehouse.Documents.Abstraction.Models.Arrival;

public sealed record ArrivalResourceData
{
    public Guid Id { get; init; }

    public string ResourceName { get; init; }

    public string MeasurementUnitName { get; init; }

    public decimal Amount { get; init; }
}
