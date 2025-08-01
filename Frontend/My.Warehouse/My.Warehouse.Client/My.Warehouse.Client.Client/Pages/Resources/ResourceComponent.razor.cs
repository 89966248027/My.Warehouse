using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using My.Warehouse.Client.Client.Models.Resources;

namespace My.Warehouse.Client.Client.Pages.Resources;

public partial class ResourceComponent : ComponentBase
{
    private const string HostName = "api/dict/resource";
    private bool _showActive = true;

    private IEnumerable<ResourceData> Data { get; set; } = [];

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
        Navigation.NavigateTo($"resource/{id}");
    }

    private async Task Load()
    {
        if (_showActive)
        {
            Data =
                await Http.GetFromJsonAsync<IEnumerable<ResourceData>>($"{HostName}/active") ?? [];
        }
        else
        {
            Data =
                await Http.GetFromJsonAsync<IEnumerable<ResourceData>>($"{HostName}/archived")
                ?? [];
        }
    }
}
