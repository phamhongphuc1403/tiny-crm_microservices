using MediatR;

namespace BuildingBlock.Domain.DomainEvent;

public interface IEvent : INotification
{
    public Guid Id => Guid.NewGuid();
    public DateTime CreatedAt => DateTime.UtcNow;
}