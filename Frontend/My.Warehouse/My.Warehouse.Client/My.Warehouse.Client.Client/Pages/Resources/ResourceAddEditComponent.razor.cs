using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using My.Warehouse.Client.Client.Models.Resources;

namespace My.Warehouse.Client.Client.Pages.Resources;

public partial class ResourceAddEditComponent : ComponentBase, IDisposable
{
    private const string HostName = "api/dict/resource";

    [Parameter]
    public Guid? id { get; set; }

    private ResourceAddEditModel Model { get; set; } = new();
    EditContext? editContext;
    private ValidationMessageStore? messageStore;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        editContext = new(Model);
        await Load();
        editContext = new(Model);
        editContext.OnValidationRequested += HandleValidationRequested;
        messageStore = new(editContext);
    }

    private void HandleValidationRequested(object? sender, ValidationRequestedEventArgs args)
    {
        messageStore?.Clear();

        if (Model.Name == null)
        {
            messageStore?.Add(() => Model.Name, "Наименование не может быть пустым");
        }
    }

    private async Task<bool> CheckUnique()
    {
        var response = await Http.PostAsJsonAsync($"{HostName}/check-unique", Model);

        if (response.IsSuccessStatusCode)
        {
            bool result = await response.Content.ReadFromJsonAsync<bool>();
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

                messageStore?.Add(() => Model.Name, "Ресурс с таким наименованием уже создан");
            }
        }
    }

    private async Task Delete()
    {
        await Http.DeleteAsync($"{HostName}/delete/{id.Value}");

        Cancel();
    }

    private async Task Archive()
    {
        await Http.DeleteAsync($"{HostName}/archive/{id.Value}");

        Cancel();
    }

    private async Task Active()
    {
        await Http.DeleteAsync($"{HostName}/active/{id.Value}");

        Cancel();
    }

    private void Cancel()
    {
        Navigation.NavigateTo("resources");
    }

    private async Task Load()
    {
        if (id.HasValue)
        {
            Model =
                await Http.GetFromJsonAsync<ResourceAddEditModel>($"{HostName}/{id.Value}")
                ?? new();
        }
    }

    public void Dispose()
    {
        if (editContext is not null)
        {
            editContext.OnValidationRequested -= HandleValidationRequested;
        }
    }
}
