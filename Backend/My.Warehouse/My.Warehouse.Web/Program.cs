using Microsoft.EntityFrameworkCore;
using My.Warehouse.Dal.Contexts;
using My.Warehouse.Web.Config;
using My.Warehouse.Web.StartupConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var frontendSettingsConfig = new FrontendSettingsConfig();
builder.Configuration.GetSection("FrontendSettings").Bind(frontendSettingsConfig);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlServerBuilder =>
        {
            sqlServerBuilder.CommandTimeout(180);
        }
    )
);

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

app.InitializeDatabase();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
