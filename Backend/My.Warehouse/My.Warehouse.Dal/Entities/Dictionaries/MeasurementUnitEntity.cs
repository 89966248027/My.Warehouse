using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.Warehouse.Dal.Entities.Documents.Arrival;
using My.Warehouse.Dal.Entities.Documents.Shipment;
using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Dal.Entities.Dictionaries;

[Table("MeasurementUnit", Schema = "dict")]
public sealed class MeasurementUnitEntity
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Name { get; set; }

    public CommonStatus Status { get; set; }

    public List<BalanceEntity> Balances { get; set; }

    public List<ArrivalResourceEntity> ArrivalResources { get; set; }

    public List<ShipmentResourceEntity> ShipmentResources { get; set; }
}
