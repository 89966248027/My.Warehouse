using My.Warehouse.Web.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var frontendSettingsConfig = new FrontendSettingsConfig();
builder.Configuration.GetSection("FrontendSettings").Bind(frontendSettingsConfig);

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(frontendSettingsConfig.HostName).AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
