using SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();


app.MapHub<DemoHub>("signalR/demo");


app.MapControllers();
app.Run();
