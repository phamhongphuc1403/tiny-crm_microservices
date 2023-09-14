using MediatR;

namespace BuildingBlock.Domain.DomainEvent;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent @event);
}