using System.ComponentModel;

namespace My.Warehouse.Client.Client.Models.Arrival;

public sealed class ArrivalResourceData
{
    public Guid Id { get; set; }

    [DisplayName("Ресурс")]
    public string ResourceName { get; set; }

    [DisplayName("Единица измерения")]
    public string MeasurementUnitName { get; set; }

    [DisplayName("Количество")]
    public decimal Amount { get; set; }
}
