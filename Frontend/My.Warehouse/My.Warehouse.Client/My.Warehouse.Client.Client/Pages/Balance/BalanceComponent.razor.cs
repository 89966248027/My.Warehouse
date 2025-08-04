using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using My.Warehouse.Client.Client.Models.Balance;
using My.Warehouse.Client.Client.Models.MeasurementUnits;
using My.Warehouse.Client.Client.Models.Resources;

namespace My.Warehouse.Client.Client.Pages.Balance;

public partial class BalanceComponent : ComponentBase
{
    private const string HostName = "api/doc/balance";

    private IEnumerable<Guid> ResourceIds { get; set; } = [];

    private IEnumerable<Guid> MeasurementUnitIds { get; set; } = [];

    private IEnumerable<ResourceDictionaryItem> Resources { get; set; }
    private IEnumerable<MeasurementUnitDictionaryItem> MeasurementUnits { get; set; }

    private IEnumerable<BalanceData> Data { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await LoadResources();
        await LoadMeasurementUnits();
        await Load();
    }

    private async Task Load()
    {
        string resources = string.Join("&", ResourceIds.Select(id => $"resourceIds={id}"));

        string measurementUnits = string.Join(
            "&",
            MeasurementUnitIds.Select(id => $"measurementUnitIds={id}")
        );

        Data =
            await Http.GetFromJsonAsync<IEnumerable<BalanceData>>(
                $"{HostName}?{resources}&{measurementUnits}"
            ) ?? [];
    }

    private async Task LoadResources()
    {
        Resources = await DictionariesService.GetResources();
    }

    private async Task LoadMeasurementUnits()
    {
        MeasurementUnits = await DictionariesService.GetMeasurementUnits();
    }
}
