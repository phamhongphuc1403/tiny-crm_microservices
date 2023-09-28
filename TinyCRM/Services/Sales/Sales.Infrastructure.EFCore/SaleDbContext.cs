using BuildingBlock.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.AccountAggregate;
using Sales.Domain.LeadAggregate;

namespace Sales.Infrastructure.EFCore;

public class SaleDbContext : BaseDbContext
{
    public SaleDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleDbContext).Assembly);
    }
}