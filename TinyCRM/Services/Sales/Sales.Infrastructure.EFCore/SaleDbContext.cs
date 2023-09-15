using BuildingBlock.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.EFCore;

public class SaleDbContext : BaseDbContext
{
    public SaleDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaleDbContext).Assembly);
    }
}