using System.ComponentModel.DataAnnotations.Schema;
using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Dal.Entities.Documents.Arrival;

[Table("ArrivalDocument", Schema = "doc")]
public sealed class ArrivalDocumentEntity
{
    public Guid Id { get; set; }

    public int Number { get; set; }

    public DateOnly DocumentDate { get; set; }

    public DocumentStatus Status { get; set; }

    public List<ArrivalResourceEntity> ArrivalResources { get; set; }
}
