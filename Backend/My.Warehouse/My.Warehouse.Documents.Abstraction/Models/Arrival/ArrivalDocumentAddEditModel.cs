using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Documents.Abstraction.Models.Arrival;

public sealed record ArrivalDocumentAddEditModel
{
    public Guid? Id { get; init; }

    public int Number { get; set; }

    public DateOnly DocumentDate { get; set; }

    public DocumentStatus Status { get; set; }

    public IEnumerable<ArrivalResourceAddEditModel> Resources { get; init; }
}
