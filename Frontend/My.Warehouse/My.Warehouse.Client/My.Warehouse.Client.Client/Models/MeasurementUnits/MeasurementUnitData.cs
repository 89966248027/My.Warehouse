using System.ComponentModel;

namespace My.Warehouse.Client.Client.Models.MeasurementUnits;

public sealed class MeasurementUnitData
{
    public Guid Id { get; set; }

    [DisplayName("Наименование")]
    public string Name { get; set; }
}
