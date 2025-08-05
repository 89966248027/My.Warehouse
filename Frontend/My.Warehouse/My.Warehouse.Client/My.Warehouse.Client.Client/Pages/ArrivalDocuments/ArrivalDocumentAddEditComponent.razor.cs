using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using My.Warehouse.Client.Client.Models.Arrival;
using My.Warehouse.Client.Client.Models.MeasurementUnits;
using My.Warehouse.Client.Client.Models.Resources;

namespace My.Warehouse.Client.Client.Pages.ArrivalDocuments;

public partial class ArrivalDocumentAddEditComponent : ComponentBase, IDisposable
{
    private const string HostName = "api/doc/arrival-document";

    [Parameter]
    public Guid? id { get; set; }

    private IEnumerable<ResourceDictionaryItem> Resources { get; set; }
    private IEnumerable<MeasurementUnitDictionaryItem> MeasurementUnits { get; set; }

    private Dictionary<Guid, Dictionary<Guid, decimal>> ResourceFundsLeft { get; set; }

    private ArrivalDocumentAddEditModel Model { get; set; } = new();
    EditContext? editContext;
    private ValidationMessageStore? messageStore;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        editContext = new(Model);
        await LoadResources();
        await LoadMeasurementUnits();
        await LoadResourceFundsLeft(id);
        await Load();
        editContext = new(Model);
        editContext.OnValidationRequested += HandleValidationRequested;
        messageStore = new(editContext);
    }

    private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        messageStore?.Clear();

        if (Model.Number == null)
        {
            messageStore?.Add(() => Model.Number, "Номер не может быть пустым");
        }

        if (Model.DocumentDate == null)
        {
            messageStore?.Add(() => Model.DocumentDate, "Дата не может быть пустым");
        }

        foreach (var resource in Model.Resources)
        {
            if (resource.ResourceId == null)
            {
                messageStore?.Add(() => resource.ResourceId, "Ресурс не может быть пустым");
            }

            if (Model.Resources.Count(x => x.ResourceId == resource.ResourceId) > 1)
            {
                messageStore?.Add(() => resource.ResourceId, "Дубль ресурса");
            }

            if (resource.MeasurementUnitId == null)
            {
                messageStore?.Add(
                    () => resource.MeasurementUnitId,
                    "Единица измерения не может быть пустым"
                );
            }

            if (resource.Amount == null)
            {
                messageStore?.Add(() => resource.Amount, "Количество не может быть пустым");
            }

            if (resource.Amount <= 0)
            {
                messageStore?.Add(() => resource.Amount, "Количество не может быть меньше 0");
            }

            if (resource.ResourceId != null && resource.MeasurementUnitId != null)
            {
                var fundsLeft = ResourceFundsLeft
                    .GetValueOrDefault(resource.ResourceId.Value)
                    ?.GetValueOrDefault(resource.MeasurementUnitId.Value);

                if (fundsLeft != null && (fundsLeft + resource.Amount) < 0)
                {
                    messageStore?.Add(
                        () => resource.Amount,
                        $"Остаток на складе не может быть меньше 0 (Остаток: {fundsLeft} + {resource.Amount} = {fundsLeft + resource.Amount})"
                    );
                }
            }
        }
    }

    private bool ValidAmounts()
    {
        IEnumerable<decimal> notValid = ResourceFundsLeft
            .Values.SelectMany(x => x.Values)
            .Where(x => x < 0)
            .ToArray();

        return !notValid.Any();
    }

    private async Task<bool> CheckUnique()
    {
        var response = await Http.PostAsJsonAsync($"{HostName}/check-unique", Model);

        if (response.IsSuccessStatusCode)
        {
            bool result = await HttpContentJsonExtensions.ReadFromJsonAsync<bool>(response.Content);
            return result;
        }

        return false;
    }

    private async Task Save()
    {
        if (editContext != null && editContext.Validate())
        {
            bool isUnique = await CheckUnique();

            if (isUnique)
            {
                var response = await Http.PostAsJsonAsync(HostName, Model);

                if (response.IsSuccessStatusCode)
                {
                    Cancel();
                }
            }
            else
            {
                messageStore?.Clear();

                messageStore?.Add(() => Model.Number, "Документ с таким номером уже создан");
            }
        }
    }

    private async Task Delete()
    {
        if (ValidAmounts())
        {
            await Http.DeleteAsync($"{HostName}/delete/{id.Value}");

            Cancel();
        }
        else
        {
            messageStore?.Clear();

            messageStore?.Add(
                () => Model.Number,
                "Невозможно удалить документ, на складе отрицательные остатки"
            );
        }
    }

    private void Cancel()
    {
        Navigation.NavigateTo("arrivals");
    }

    private async Task Load()
    {
        if (id.HasValue)
        {
            Model =
                await HttpClientJsonExtensions.GetFromJsonAsync<ArrivalDocumentAddEditModel>(
                    Http,
                    $"{HostName}/{id.Value}"
                ) ?? new();
        }
    }

    private async Task LoadResources()
    {
        Resources = await DictionariesService.GetResources();
    }

    private async Task LoadMeasurementUnits()
    {
        MeasurementUnits = await DictionariesService.GetMeasurementUnits();
    }

    private async Task LoadResourceFundsLeft(Guid? documentId)
    {
        ResourceFundsLeft = await DictionariesService.GetResourceFundsLeftWithoutDocument(
            documentId
        );
    }

    private void AddItem()
    {
        Model.Resources.Add(new ArrivalResourceAddEditModel());
    }

    private void RemoveItem(int index)
    {
        Model.Resources.RemoveAt(index);
    }

    public void Dispose()
    {
        if (editContext is not null)
        {
            editContext.OnValidationRequested -= HandleValidationRequested;
        }
    }
}
