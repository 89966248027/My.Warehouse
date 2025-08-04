using System.ComponentModel;

namespace My.Warehouse.Client.Client.Models.Balance;

public sealed class BalanceData
{
    public Guid Id { get; set; }

    [DisplayName("Ресурс")]
    public required string ResourceName { get; set; }

    [DisplayName("Единица измерения")]
    public required string MeasurementUnitName { get; set; }

    [DisplayName("Количество")]
    public decimal Amount { get; set; }
}
