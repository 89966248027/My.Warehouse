using System.Net.Http.Json;
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
}
