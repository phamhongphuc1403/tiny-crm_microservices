using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Infrastructure.Serilog;
using BuildingBlock.Presentation.Middleware;
using People.API.Extensions;
using People.API.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = ApplicationLoggerFactory.CreateSerilogLogger(builder.Configuration, "PeopleService");

await builder.Services.AddDefaultExtensions(builder.Configuration);
builder.Host.UseSerilog();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
var environment = app.Services.GetRequiredService<IWebHostEnvironment>();

app.UseCustomerExceptionHandler(environment);
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var eventBus = app.Services.GetRequiredService<IEventBus>();

app.SubscribeIntegrationEvents(eventBus);

await app.SeedDataAsync();

app.Run();