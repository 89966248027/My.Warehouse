using System.ComponentModel;

namespace My.Warehouse.Client.Client.Models.Clients;

public sealed class ClientData
{
    public Guid Id { get; set; }

    [DisplayName("Наименование")]
    public string Name { get; set; }

    [DisplayName("Адрес")]
    public string? Address { get; set; }
}
