using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.Warehouse.Dal.Entities.Documents.Shipment;
using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Dal.Entities.Dictionaries;

[Table("Client", Schema = "dict")]
public sealed class ClientEntity
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    [Required]
    [MaxLength(250)]
    public string Address { get; set; }

    public CommonStatus Status { get; set; }

    public List<ShipmentDocumentEntity> ShipmentDocuments { get; set; }
}
