using System.ComponentModel.DataAnnotations.Schema;

namespace My.Warehouse.Dal.Entities.Dictionaries;

[Table("Balance", Schema = "dict")]
public sealed class BalanceEntity
{
    public Guid Id { get; set; }

    public Guid ResourceId { get; set; }

    public ResourceEntity Resource { get; set; }

    public Guid MeasurementUnitId { get; set; }

    public MeasurementUnitEntity MeasurementUnit { get; set; }

    public decimal Amount { get; set; }
}
