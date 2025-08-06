using System.ComponentModel;

namespace My.Warehouse.Client.Client.Models.Shipment;

public sealed class ShipmentDocumentData
{
    public Guid Id { get; set; }

    [DisplayName("Номер")]
    public int Number { get; set; }

    [DisplayName("Клиент")]
    public string ClientName { get; set; }

    [DisplayName("Дата")]
    public DateOnly DocumentDate { get; set; }

    public IEnumerable<ShipmentResourceData> Resources { get; set; }
}
