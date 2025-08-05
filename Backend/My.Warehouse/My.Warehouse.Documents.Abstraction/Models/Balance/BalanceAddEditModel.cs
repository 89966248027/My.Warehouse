namespace My.Warehouse.Documents.Abstraction.Models.Balance;

public sealed record BalanceAddEditModel
{
    public Guid Id { get; init; }

    public Guid ResourceId { get; init; }

    public Guid MeasurementUnitId { get; init; }

    public decimal Amount { get; init; }
}
