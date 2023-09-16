using BuildingBlock.Presentation.Extensions;
using BuildingBlock.Presentation.Middleware;
using IAM.API.Extensions;
using IAM.Infrastructure.Grpc.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80, listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });
    options.ListenAnyIP(50051, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabase(builder.Configuration)
    .AddAuthentication(builder.Configuration)
    .AddDefaultOpenApi(builder.Configuration)
    .AddMapper()
    .AddServices()
    .AddRedisCache(builder.Configuration);

builder.Services.AddGrpc();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var environment = app.Services.GetRequiredService<IWebHostEnvironment>();

app.UseCustomerExceptionHandler(environment);

app.UseAuthorization();

app.MapGrpcService<GreeterService>();
app.MapControllers();
await app.ApplyMigrationAsync();
await app.SeedDataAsync();
app.Run();