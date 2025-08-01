using My.Warehouse.Dal.Enums;

namespace My.Warehouse.Dictionaries.Abstraction.Models.Clients;

public sealed record ClientAddEditModel
{
    public Guid? Id { get; init; }

    public string Name { get; init; }

    public string Address { get; init; }

    public CommonStatus Status { get; init; }

    public bool HasDeleted { get; init; }
}
