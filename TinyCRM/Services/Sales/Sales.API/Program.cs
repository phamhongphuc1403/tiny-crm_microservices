using BuildingBlock.Presentation.Extensions;
using Sales.API.Extensions;
using Sales.Application;
using Sales.Infrastructure.EFCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services
    .AddSwagger()
    .AddCqrs<SalesApplicationAssemblyReference>()
    .AddMapper<Mapper>()
    .AddDatabase<SaleDbContext>(builder.Configuration)
    .AddDependencyInjection()
    ;

await builder.Services.ApplyMigrationAsync<SaleDbContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();