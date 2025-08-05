using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Documents.Abstraction.Models.Balance;

public sealed record BalanceFundsLeft
{
    public Guid ResourceId { get; init; }

    public Guid MeasurementUnitId { get; init; }

    public decimal Amount { get; init; }

    public DocumentType DocumentType { get; init; }
}
