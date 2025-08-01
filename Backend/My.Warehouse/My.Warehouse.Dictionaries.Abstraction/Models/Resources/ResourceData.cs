namespace My.Warehouse.Dictionaries.Abstraction.Models.Resources;

public sealed record ResourceData
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}
