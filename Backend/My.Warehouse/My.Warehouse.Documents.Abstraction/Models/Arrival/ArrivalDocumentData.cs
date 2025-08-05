namespace My.Warehouse.Documents.Abstraction.Models.Arrival;

public sealed record ArrivalDocumentData
{
    public Guid Id { get; init; }

    public int Number { get; init; }

    public DateOnly DocumentDate { get; init; }

    public IEnumerable<ArrivalResourceData> Resources { get; init; }
}
