using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensionsa;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationService()
    .AddInfrastructureService(builder.Configuration)
    .AddAPIServices();
var app = builder.Build();

app.UseAPIServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.MapGet("/", () => "Hello World!");

app.Run();
