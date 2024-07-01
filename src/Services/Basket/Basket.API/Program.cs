using Basket.API.Data;
using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using FluentValidation;
using HealthChecks.UI.Client;
using Discount.Grpc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    configuration.AddOpenBehavior(typeof(LoggerBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("Database")!);
    opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
   
});
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["GrpcConfigs:DiscountUrl"]);
});
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
 .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
 .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions { ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse});

app.Run();
