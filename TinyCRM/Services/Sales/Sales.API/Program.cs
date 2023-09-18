using BuildingBlock.Presentation.Middleware;
using Sales.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions(builder.Configuration);

var app = builder.Build();

var environment = app.Services.GetRequiredService<IWebHostEnvironment>();

app.UseCustomerExceptionHandler(environment);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();