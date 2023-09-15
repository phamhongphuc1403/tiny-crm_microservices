using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EFCore;

public class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options) : base(options)
    {
    }
}