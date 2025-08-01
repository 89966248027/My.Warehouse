namespace My.Warehouse.Dictionaries.Abstraction.Models.Clients;

public sealed record ClientData
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public string Address { get; set; }
}
