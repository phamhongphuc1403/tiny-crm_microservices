using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.Application.Identity;
using BuildingBlock.Application.IntegrationEvents.Handlers;
using BuildingBlock.Presentation.Authentication;
using BuildingBlock.Presentation.Middleware;
using Sales.API.Extensions;
using Sales.Application.IntegrationEvents.Events;
using Sales.Application.IntegrationEvents.Handlers;

var builder = WebApplication.CreateBuilder(args);

await builder.Services.AddDefaultExtensions(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services
    .AddScoped<IIntegrationEventHandler<AccountCreatedIntegrationEvent>,
        AccountCreatedIntegrationEventHandler>();
builder.Services
    .AddScoped<IIntegrationEventHandler<AccountEditedIntegrationEvent>,
        AccountEditedIntegrationEventHandler>();
builder.Services
    .AddScoped<IIntegrationEventHandler<AccountsDeletedIntegrationEvent>,
        AccountsDeletedIntegrationEventHandler>();

var app = builder.Build();

var environment = app.Services.GetRequiredService<IWebHostEnvironment>();

app.UseCustomerExceptionHandler(environment);

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<AccountCreatedIntegrationEvent,
    IIntegrationEventHandler<AccountCreatedIntegrationEvent>>();
eventBus.Subscribe<AccountEditedIntegrationEvent,
    IIntegrationEventHandler<AccountEditedIntegrationEvent>>();
eventBus.Subscribe<AccountsDeletedIntegrationEvent,
    IIntegrationEventHandler<AccountsDeletedIntegrationEvent>>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();