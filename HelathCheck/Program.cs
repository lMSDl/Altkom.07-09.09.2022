using HealthChecks.UI.Client;
using HelathCheck;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<RandomHealth>(nameof(RandomHealth))
    .AddCheck<DirectoryAccess>(nameof(DirectoryAccess))
    .AddSqlServer("Data source=(local)\\SQLEXPRESS;Database=test;Integrated security=true");
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();



app.MapHealthChecks("/Health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.MapGet("/", () => "Hello World!");

app.Run();
