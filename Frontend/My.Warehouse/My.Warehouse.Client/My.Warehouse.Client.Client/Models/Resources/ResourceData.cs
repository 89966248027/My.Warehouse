using System.ComponentModel;

namespace My.Warehouse.Client.Client.Models.Resources;

public sealed class ResourceData
{
    public Guid Id { get; set; }

    [DisplayName("Наименование")]
    public string Name { get; set; }
}
