using BuildingBlock.Application.EventBus.Interfaces;
using People.Application.IntegrationEvents.Events;
using People.Application.IntegrationEvents.Handlers;

namespace People.API.Middlewares;

public static class EventBusMiddleware
{
    public static IApplicationBuilder SubscribeIntegrationEvents(this IApplicationBuilder app, IEventBus eventBus)
    {
        eventBus.Subscribe<AccountSaleCreatedIntegrationEvent, AccountSaleCreatedIntegrationEventHandler>();
        eventBus.Subscribe<ClosedWonDealIntegrationEvent,ClosedWonDealIntegrationEventHandler>();
        eventBus.Subscribe<DeletedWonDealIntegrationEvent,DeletedWonDealIntegrationEventHandler>();
        return app;
    }
}