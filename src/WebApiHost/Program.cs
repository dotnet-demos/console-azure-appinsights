using Microsoft.ApplicationInsights.Extensibility;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Calculator>();
builder.Services.AddSingleton<ITelemetryInitializer>((serviceProvider) =>
{
    return new CloudRoleNameTelemetryInitializer("webApiHost");
});
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();
ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
ILogger logger = loggerFactory.CreateLogger("MinimalAPI");
logger.LogTrace("This is a sample trace");
logger.LogDebug("This is a sample debug");
logger.LogInformation("This is a sample info");
logger.LogWarning("This is a sample warning");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/api/circle/areaOf/{radius}", async (int radius,Calculator calc, ILoggerFactory loggerFactory) =>
{
    ILogger localLogger = loggerFactory.CreateLogger("CircleAPI");
    localLogger.LogInformation($"api/circle/areaOf/{radius} - Started.");
    return await calc.AreaOfCircle(radius);
}).WithName("AreaOfCircle");
app.Run();
