namespace My.Warehouse.Client.Client.Models.Shipment;

public sealed class ShipmentResourceAddEditModel
{
    public Guid? Id { get; set; }

    public Guid? ResourceId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Amount { get; set; }
}
