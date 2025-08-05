using My.Warehouse.Client.Client.Enums;

namespace My.Warehouse.Client.Client.Models.Arrival;

public sealed class ArrivalDocumentAddEditModel
{
    public Guid? Id { get; set; }

    public int? Number { get; set; }

    public DateOnly DocumentDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    public DocumentStatus Status { get; set; }

    public List<ArrivalResourceAddEditModel> Resources { get; set; } = [];
}
