using BuildingBlock.Application.Identity;
using BuildingBlock.Infrastructure.Serilog;
using BuildingBlock.Presentation.Authentication;
using BuildingBlock.Presentation.Extensions;
using BuildingBlock.Presentation.Middleware;
using FluentValidation;
using IAM.API.Extensions;
using IAM.API.GRPC.Services;
using IAM.Business;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "IamService");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabase(builder.Configuration)
    .AddAuthentication(builder.Configuration)
    .AddAuthorizations()
    .AddDefaultOpenApi(builder.Configuration)
    .AddMapper()
    .AddValidatorsFromAssembly(typeof(IdentityBusinessAssemblyReference).Assembly)
    .AddServices()
    .AddRedisCacheIam(builder.Configuration);

builder.Host.UseSerilog();

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
await app.SeedDataAsync();
app.Run();