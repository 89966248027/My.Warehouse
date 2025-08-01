using My.Warehouse.Client.Client.Enums;

namespace My.Warehouse.Client.Client.Models.MeasurementUnits;

public sealed class MeasurementUnitAddEditModel
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public CommonStatus Status { get; set; }

    public bool HasDeleted { get; init; }
}
