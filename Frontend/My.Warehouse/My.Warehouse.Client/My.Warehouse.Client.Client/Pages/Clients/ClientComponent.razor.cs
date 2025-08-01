using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using My.Warehouse.Client.Client.Models.Clients;

namespace My.Warehouse.Client.Client.Pages.Clients;

public partial class ClientComponent : ComponentBase
{
    private const string HostName = "api/dict/client";
    private bool _showActive = true;

    private IEnumerable<ClientData> Data { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await Load();
    }

    private void Add()
    {
        Open(null);
    }

    private async Task OnArchived()
    {
        _showActive = !_showActive;
        await Load();
    }

    private void Open(Guid? id)
    {
        Navigation.NavigateTo($"client/{id}");
    }

    private async Task Load()
    {
        if (_showActive)
        {
            Data = await Http.GetFromJsonAsync<IEnumerable<ClientData>>($"{HostName}/active") ?? [];
        }
        else
        {
            Data =
                await Http.GetFromJsonAsync<IEnumerable<ClientData>>($"{HostName}/archived") ?? [];
        }
    }
}
