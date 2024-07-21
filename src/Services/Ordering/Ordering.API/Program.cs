using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationService(builder.Configuration)
    .AddInfrastructureService(builder.Configuration)
    .AddAPIServices(builder.Configuration);
var app = builder.Build();

app.UseAPIServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

app.MapGet("/", () => "Hello World!");

app.Run();
