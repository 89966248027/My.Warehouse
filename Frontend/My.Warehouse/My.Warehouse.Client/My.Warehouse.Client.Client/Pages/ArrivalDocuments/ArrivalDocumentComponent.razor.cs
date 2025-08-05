using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using My.Warehouse.Client.Client.Models.Arrival;
using My.Warehouse.Client.Client.Models.MeasurementUnits;
using My.Warehouse.Client.Client.Models.Resources;

namespace My.Warehouse.Client.Client.Pages.ArrivalDocuments;

public partial class ArrivalDocumentComponent : ComponentBase
{
    private const string HostName = "api/doc/arrival-document";

    private IEnumerable<Guid> ResourceIds { get; set; } = [];
    private IEnumerable<Guid> MeasurementUnitIds { get; set; } = [];
    private int? Number { get; set; }
    private DateOnly?[] DateRange { get; set; }

    private IEnumerable<ResourceDictionaryItem> Resources { get; set; }
    private IEnumerable<MeasurementUnitDictionaryItem> MeasurementUnits { get; set; }

    private IEnumerable<ArrivalDocumentData> Data { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await LoadResources();
        await LoadMeasurementUnits();
        await Load();
    }

    private void Add()
    {
        Open(null);
    }

    private void Open(Guid? id)
    {
        Navigation.NavigateTo($"arrival/{id}");
    }

    private async Task Load()
    {
        string resources = string.Join("&", ResourceIds.Select(id => $"resourceIds={id}"));

        string measurementUnits = string.Join(
            "&",
            MeasurementUnitIds.Select(id => $"measurementUnitIds={id}")
        );

        DateOnly? from = DateRange[0];
        DateOnly? to = DateRange[1];

        Data =
            await Http.GetFromJsonAsync<IEnumerable<ArrivalDocumentData>>(
                $"{HostName}?from={from}&to={to}&number={Number}&{resources}&{measurementUnits}"
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
