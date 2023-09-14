using BuildingBlock.Domain.Helper.Specification;

namespace BuildingBlock.Domain.Interfaces;

public interface IRepository<TEntity, in TKey> where TEntity : class
{
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void Update(TEntity entity);

    Task<TEntity?> GetAsync(TKey id, string? stringInclude = null);

    Task<List<TEntity>> ListAsync(ISpecification<TEntity> specification, string? includeTables = null,
        string? sorting = null, int pageIndex = 1, int pageSize = int.MaxValue);

    Task<bool> AnyAsync(TKey id);

    Task<bool> AnyAsync();
}