using BuildingBlock.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using People.Domain.AccountAggregate.Entities;
using People.Domain.ContactAggregate.Entities;

namespace People.Infrastructure.EFCore;

public class PeopleDbContext : BaseDbContext
{
    public PeopleDbContext(DbContextOptions options) : base(options)
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