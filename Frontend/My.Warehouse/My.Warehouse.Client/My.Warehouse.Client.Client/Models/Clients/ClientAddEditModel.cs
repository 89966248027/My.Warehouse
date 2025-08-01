using My.Warehouse.Client.Client.Enums;

namespace My.Warehouse.Client.Client.Models.Clients;

public sealed class ClientAddEditModel
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public CommonStatus Status { get; set; }

    public string? Address { get; set; }

    public bool HasDeleted { get; init; }
}
