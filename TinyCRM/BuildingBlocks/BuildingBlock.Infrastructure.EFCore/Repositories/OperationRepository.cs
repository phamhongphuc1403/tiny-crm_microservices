using BuildingBlock.Domain.Model;
using BuildingBlock.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EFCore.Repositories;

public class OperationRepository<TDbContext, TEntity> : IOperationRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : GuidEntity
{
    private readonly TDbContext _dbContext;
    private DbSet<TEntity>? _dbSet;

    public OperationRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var guidEntities = entities.ToList();

        foreach (var entity in guidEntities) entity.CreatedDate = DateTime.UtcNow;

        await DbSet.AddRangeAsync(guidEntities);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        DbSet.Update(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
}