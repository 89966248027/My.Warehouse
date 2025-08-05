using System.ComponentModel;

namespace My.Warehouse.Client.Client.Models.Arrival;

public sealed class ArrivalDocumentData
{
    public Guid Id { get; set; }

    [DisplayName("Номер")]
    public int Number { get; set; }

    [DisplayName("Дата")]
    public DateOnly DocumentDate { get; set; }

    public IEnumerable<ArrivalResourceData> Resources { get; set; }
}
