using Microsoft.Extensions.DependencyInjection;
using My.Warehouse.Dictionaries.Abstraction.Repositories;
using My.Warehouse.Dictionaries.Abstraction.Services;
using My.Warehouse.Dictionaries.Repositories;
using My.Warehouse.Dictionaries.Services;

namespace My.Warehouse.Dictionaries;

public static class DiExtensions
{
    public static IServiceCollection AddDictionaries(this IServiceCollection services)
    {
        return services
            .AddScoped<IResourceRepository, ResourceRepository>()
            .AddScoped<IResourceService, ResourceService>()
            .AddScoped<IMeasurementUnitRepository, MeasurementUnitRepository>()
            .AddScoped<IMeasurementUnitService, MeasurementUnitService>();
    }
}
