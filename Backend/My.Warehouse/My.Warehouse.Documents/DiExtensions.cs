using Microsoft.Extensions.DependencyInjection;
using My.Warehouse.Documents.Abstraction.Repositories;
using My.Warehouse.Documents.Abstraction.Services;
using My.Warehouse.Documents.Repositories;
using My.Warehouse.Documents.Services;

namespace My.Warehouse.Documents;

public static class DiExtensions
{
    public static IServiceCollection AddDocuments(this IServiceCollection services)
    {
        return services
            .AddScoped<IBalanceRepository, BalanceRepository>()
            .AddScoped<IBalanceService, BalanceService>()
            .AddScoped<IArrivalDocumentRepository, ArrivalDocumentRepository>()
            .AddScoped<IArrivalDocumentService, ArrivalDocumentService>()
            .AddScoped<IShipmentDocumentRepository, ShipmentDocumentRepository>()
            .AddScoped<IShipmentDocumentService, ShipmentDocumentService>();
    }
}
