using BuildingBlock.Presentation.Extensions;
using BuildingBlock.Presentation.Middleware;
using IAM.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDatabase(builder.Configuration)
    .AddAuthentication(builder.Configuration)
    .AddSwagger()
    .AddMapper()
    .AddServices();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var environment = app.Services.GetRequiredService<IWebHostEnvironment>();
    
app.UseCustomerExceptionHandler(environment);

app.UseAuthorization();

app.MapControllers();
await app.ApplyMigrationAsync();
await app.SeedDataAsync();
app.Run();