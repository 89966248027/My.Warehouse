namespace My.Warehouse.Documents.Abstraction.Models.Arrival;

public sealed record ArrivalResourceAddEditModel
{
    public Guid? Id { get; init; }

    public Guid ResourceId { get; init; }

    public Guid MeasurementUnitId { get; init; }

    public decimal Amount { get; init; }
}
