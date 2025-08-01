using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using My.Warehouse.Client.Client.Models.MeasurementUnits;

namespace My.Warehouse.Client.Client.Pages.MeasurementUnits;

public partial class MeasurementUnitComponent : ComponentBase
{
    private const string HostName = "api/dict/measurement-unit";
    private bool _showActive = true;

    private IEnumerable<MeasurementUnitData> Data { get; set; } = [];

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
        Navigation.NavigateTo($"measurement-unit/{id}");
    }

    private async Task Load()
    {
        if (_showActive)
        {
            Data =
                await HttpClientJsonExtensions.GetFromJsonAsync<IEnumerable<MeasurementUnitData>>(
                    Http,
                    $"{HostName}/active"
                ) ?? [];
        }
        else
        {
            Data =
                await HttpClientJsonExtensions.GetFromJsonAsync<IEnumerable<MeasurementUnitData>>(
                    Http,
                    $"{HostName}/archived"
                ) ?? [];
        }
    }
}
