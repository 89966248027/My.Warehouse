using System.ComponentModel.DataAnnotations.Schema;
using My.Warehouse.Dal.Entities.Dictionaries;

namespace My.Warehouse.Dal.Entities.Documents.Shipment;

[Table("ShipmentResource", Schema = "doc")]
public sealed class ShipmentResourceEntity
{
    public Guid Id { get; set; }

    public Guid ResourceId { get; set; }

    public ResourceEntity Resource { get; set; }

    public Guid MeasurementUnitId { get; set; }

    public MeasurementUnitEntity MeasurementUnit { get; set; }

    public decimal Amount { get; set; }

    public Guid ShipmentDocumentId { get; set; }

    public ShipmentDocumentEntity ShipmentDocument { get; set; }
}
