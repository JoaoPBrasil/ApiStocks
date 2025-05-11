
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddHttpClient<BrapiService>();
builder.Services.AddSingleton<BrapiService>();

var app = builder.Build();

app.UseCors();

app.MapGet("/stocks", async (BrapiService service) =>
{
    var symbols = new[] { "PETR4", "VALE3", "ITUB4", "BBDC4", "BBAS3", "ABEV3", "WEGE3" };
    var data = await service.GetStockDataAsync(symbols);
    return Results.Ok(data);
});

app.Run();
