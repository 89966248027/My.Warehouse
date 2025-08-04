using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using My.Warehouse.Client.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:45200"),
});

builder.Services.AddScoped<DictionariesService>();

builder.Services.AddAntDesign();

await builder.Build().RunAsync();
