using System.Net.Http.Json;
using My.Warehouse.Client.Client.Models.Clients;
using My.Warehouse.Client.Client.Models.MeasurementUnits;
using My.Warehouse.Client.Client.Models.Resources;

namespace My.Warehouse.Client.Client.Services;

public sealed class DictionariesService
{
    private readonly HttpClient _httpClient;

    public DictionariesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private const string ApiResource = "api/dict/resource";
    private const string ApiMeasurementUnit = "api/dict/measurement-unit";
    private const string ApiClient = "api/dict/client";
    private const string ApiBalance = "api/doc/balance";

    public async Task<IEnumerable<ResourceDictionaryItem>> GetResources()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ResourceDictionaryItem>>(
                $"{ApiResource}"
            ) ?? [];
    }

    public async Task<IEnumerable<MeasurementUnitDictionaryItem>> GetMeasurementUnits()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<MeasurementUnitDictionaryItem>>(
                $"{ApiMeasurementUnit}"
            ) ?? [];
    }

    public async Task<IEnumerable<ClientDictionaryItem>> GetClients()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ClientDictionaryItem>>($"{ApiClient}")
            ?? [];
    }

    public async Task<Dictionary<Guid, Dictionary<Guid, decimal>>> GetResourceFundsLeft()
    {
        return await _httpClient.GetFromJsonAsync<Dictionary<Guid, Dictionary<Guid, decimal>>>(
                $"{ApiBalance}/funds-left"
            ) ?? [];
    }

    public async Task<
        Dictionary<Guid, Dictionary<Guid, decimal>>
    > GetResourceFundsLeftWithoutDocument(Guid? documentId)
    {
        return await _httpClient.GetFromJsonAsync<Dictionary<Guid, Dictionary<Guid, decimal>>>(
                $"{ApiBalance}/funds-left/without-document?documentId={documentId}"
            ) ?? [];
    }
}
