namespace My.Warehouse.Documents.Abstraction.Models.Balance;

public sealed record BalanceData
{
    public Guid Id { get; init; }

    public required string ResourceName { get; init; }

    public required string MeasurementUnitName { get; init; }

    public decimal Amount { get; init; }
}
