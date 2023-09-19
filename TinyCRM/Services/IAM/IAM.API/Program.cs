using BuildingBlock.Application.Identity;
using BuildingBlock.Presentation.Authentication;
using BuildingBlock.Presentation.Extensions;
using BuildingBlock.Presentation.Middleware;
using IAM.API.Extensions;
using IAM.Infrastructure.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabase(builder.Configuration)
    .AddAuthentication(builder.Configuration)
    .AddAuthorizations()
    .AddDefaultOpenApi(builder.Configuration)
    .AddMapper()
    .AddServices()
    .AddRedisCache(builder.Configuration);
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddGrpc();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var environment = app.Services.GetRequiredService<IWebHostEnvironment>();

app.UseCustomerExceptionHandler(environment);
app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<AuthServer>();
app.MapControllers();
await app.ApplyMigrationAsync();
await app.SeedIamDataAsync();
app.Run();