using System.Text.Json.Serialization;
using BuildingBlock.Presentation.Extensions;
using BuildingBlock.Presentation.Middleware;
using Sales.API.Extensions;
using Sales.Application;
using Sales.Infrastructure.EFCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services
    .AddDefaultOpenApi(builder.Configuration)
    .AddCqrs<SalesApplicationAssemblyReference>()
    .AddMapper<Mapper>()
    .AddDatabase<SaleDbContext>(builder.Configuration)
    .AddDependencyInjection()
    ;

await builder.Services.ApplyMigrationAsync<SaleDbContext>();

var app = builder.Build();

var environment = app.Services.GetRequiredService<IWebHostEnvironment>();

app.UseCustomerExceptionHandler(environment);

app.UseSwagger();
app.UseSwaggerUI(c => c.InjectStylesheet("/swagger/custom.css"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();