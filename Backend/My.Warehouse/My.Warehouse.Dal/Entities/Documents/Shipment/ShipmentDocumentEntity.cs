using System.ComponentModel.DataAnnotations.Schema;
using My.Warehouse.Dal.Entities.Dictionaries;
using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Dal.Entities.Documents.Shipment;

[Table("ShipmentDocument", Schema = "doc")]
public sealed class ShipmentDocumentEntity
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public DateOnly DocumentDate { get; set; }

    public Guid ClientId { get; set; }

    public ClientEntity Client { get; set; }

    public DocumentStatus Status { get; set; }

    public List<ShipmentResourceEntity> ShipmentResources { get; set; }
}
