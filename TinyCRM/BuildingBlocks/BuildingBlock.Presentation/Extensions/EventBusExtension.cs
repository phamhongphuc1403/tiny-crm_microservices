using BuildingBlock.Application.EventBus;
using BuildingBlock.Application.EventBus.Interfaces;
using BuildingBlock.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BuildingBlock.Presentation.Extensions;

public static class EventBusExtension
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //   "EventBus": {
        //     "SubscriptionClientName": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "RetryCount": 1
        //   }
        // }

        var eventBusSection = configuration.GetSection("EventBus");

        if (!eventBusSection.Exists()) return services;

        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory
            {
                HostName = eventBusSection["HostName"] ?? "tinycrm.rabbitmq",
                UserName = eventBusSection["UserName"],
                Password = eventBusSection["Password"],
                Port = int.Parse(eventBusSection["Port"] ?? "5672"),
                DispatchConsumersAsync = true
            };

            var retryCount = eventBusSection.GetValue("RetryCount", 5);

            return new RabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
        {
            var subscriptionClientName = eventBusSection.GetRequiredValue("SubscriptionClientName");
            ArgumentNullException.ThrowIfNull(subscriptionClientName);

            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var retryCount = eventBusSection.GetValue("RetryCount", 5);

            return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager,
                subscriptionClientName, retryCount);
        });

        services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();

        return services;
    }
}