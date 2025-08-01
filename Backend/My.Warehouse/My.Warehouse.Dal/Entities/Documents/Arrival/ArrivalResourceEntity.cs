using System.ComponentModel.DataAnnotations.Schema;
using My.Warehouse.Dal.Entities.Dictionaries;

namespace My.Warehouse.Dal.Entities.Documents.Arrival;

[Table("ArrivalResource", Schema = "doc")]
public sealed class ArrivalResourceEntity
{
    public Guid Id { get; set; }

    public Guid ResourceId { get; set; }

    public ResourceEntity Resource { get; set; }

    public Guid MeasurementUnitId { get; set; }

    public MeasurementUnitEntity MeasurementUnit { get; set; }

    public decimal Amount { get; set; }

    public Guid ArrivalDocumentId { get; set; }

    public ArrivalDocumentEntity ArrivalDocument { get; set; }
}
