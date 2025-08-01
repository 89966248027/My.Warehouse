using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Dictionaries.Abstraction.Models.MeasurementUnits;

public sealed record MeasurementUnitAddEditModel
{
    public Guid? Id { get; init; }

    public string Name { get; init; }

    public CommonStatus Status { get; init; }

    public bool HasDeleted { get; init; }
}
