namespace My.Warehouse.Dictionaries.Abstraction.Models.MeasurementUnits;

public sealed record MeasurementUnitData
{
    public Guid Id { get; init; }

    public string Name { get; init; }
}
