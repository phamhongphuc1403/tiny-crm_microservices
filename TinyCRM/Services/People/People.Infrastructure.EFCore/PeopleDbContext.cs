using BuildingBlock.Infrastructure.EFCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using People.Domain.AccountAggregate.Entities;
using People.Domain.ContactAggregate.Entities;

namespace People.Infrastructure.EFCore;

public class PeopleDbContext : BaseDbContext
{
    public PeopleDbContext(DbContextOptions options, IMediator mediator, ILogger<BaseDbContext> logger) : base(options,
        mediator, logger)
    {
    }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PeopleDbContext).Assembly);
    }
}