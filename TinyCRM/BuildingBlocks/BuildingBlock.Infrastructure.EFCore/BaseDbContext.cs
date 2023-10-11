using BuildingBlock.Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Infrastructure.EFCore;

public class BaseDbContext : DbContext
{
    private readonly ILogger<BaseDbContext> _logger;
    private readonly IMediator _mediator;

    protected BaseDbContext(DbContextOptions options, IMediator mediator, ILogger<BaseDbContext> logger) : base(options)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        await DispatchDomainEventsAsync();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<GuidEntity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents!)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("Dispatching domain event. Event {eventName}: {eventId}", domainEvent.GetType().Name,
                domainEvent.Id);
            await _mediator.Publish(domainEvent);
        }
    }
}