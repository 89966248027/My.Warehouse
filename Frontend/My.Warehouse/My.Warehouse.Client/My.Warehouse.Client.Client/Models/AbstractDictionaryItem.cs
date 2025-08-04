namespace My.Warehouse.Client.Client.Models;

public abstract class AbstractDictionaryItem
{
    public Guid Id { get; init; }

    public string Name { get; init; }

    public bool IsDisable { get; init; }
}
