using BuildingBlock.Infrastructure.EFCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sales.Domain.AccountAggregate;
using Sales.Domain.LeadAggregate;

namespace Sales.Infrastructure.EFCore;

public class SaleDbContext : BaseDbContext
{
    public DbSet<Lead> Leads { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleDbContext).Assembly);
    }

    public SaleDbContext(DbContextOptions options, IMediator mediator, ILogger<BaseDbContext> logger) : base(options, mediator, logger)
    {
    }
}