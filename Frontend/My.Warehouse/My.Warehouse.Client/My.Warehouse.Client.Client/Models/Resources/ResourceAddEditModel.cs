using My.Warehouse.Client.Client.Enums;

namespace My.Warehouse.Client.Client.Models.Resources;

public sealed class ResourceAddEditModel
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public CommonStatus Status { get; set; }

    public bool HasDeleted { get; init; }
}
