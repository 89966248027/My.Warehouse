namespace My.Warehouse.Client.Client.Models.Arrival;

public sealed class ArrivalResourceAddEditModel
{
    public Guid? Id { get; set; }

    public Guid? ResourceId { get; set; }

    public Guid? MeasurementUnitId { get; set; }

    public decimal? Amount { get; set; }
}
