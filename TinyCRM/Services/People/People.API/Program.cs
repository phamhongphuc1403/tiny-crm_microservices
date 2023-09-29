using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Presentation.Middleware;
using People.API.Extensions;
using People.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions(builder.Configuration);

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