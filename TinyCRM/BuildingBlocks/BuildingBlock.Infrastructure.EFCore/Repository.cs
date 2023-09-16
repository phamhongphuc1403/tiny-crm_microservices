using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using BuildingBlock.Domain.Interfaces;
using BuildingBlock.Domain.Model;
using BuildingBlock.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infrastructure.EFCore;

public class Repository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TDbContext : DbContext
    where TEntity : class
{
    private readonly TDbContext _dataContext;
    private DbSet<TEntity>? _dbSet;

    public Repository(TDbContext dataContext)
    {
        _dataContext = dataContext;
    }

    protected DbSet<TEntity> DbSet => _dbSet ??= _dataContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
    {
        if (entity is IAuditEntity auditEntity) auditEntity.CreatedDate = DateTime.UtcNow;
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        if (entity is IAuditEntity auditEntity) auditEntity.UpdatedDate = DateTime.UtcNow;
        DbSet.Update(entity);
    }

    public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, string? includeTables = null,
        string? sorting = null, int pageIndex = 1, int pageSize = int.MaxValue)
    {
        IQueryable<TEntity> query = DbSet;

        query = Including(query, includeTables);

        query = Filter(query, specification);

        query = Sorting(query, sorting);

        query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        return await query.ToListAsync();
    }

    public Task<TEntity?> GetAsync(TKey id, string? includeTables = null)
    {
        IQueryable<TEntity> query = DbSet;

        query = query.Where(ExpressionForGet(id));

        if (string.IsNullOrEmpty(includeTables)) return query.FirstOrDefaultAsync();
        var includeProperties = includeTables.Split(',');

        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return query.FirstOrDefaultAsync();
    }

    public virtual Task<bool> AnyAsync(TKey id)
    {
        return DbSet.AnyAsync();
    }

    public Task<bool> AnyAsync()
    {
        return DbSet.AnyAsync();
    }

    private static IQueryable<TEntity> Filter(IQueryable<TEntity> query, ISpecification<TEntity> specification)
    {
        query = query.Where(specification.ToExpression());
        return query;
    }

    private static IQueryable<TEntity> Including(IQueryable<TEntity> query, string? includeTables = null)
    {
        if (string.IsNullOrEmpty(includeTables)) return query;
        var includeProperties = includeTables.Split(',');

        query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        return query;
    }

    private static IQueryable<TEntity> Sorting(IQueryable<TEntity> query, string? sorting)
    {
        return string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
    }

    protected virtual Expression<Func<TEntity, bool>> ExpressionForGet(TKey id)
    {
        return p => true;
    }
}