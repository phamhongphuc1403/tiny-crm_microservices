using BuildingBlock.Domain.Helper.Specification;
using BuildingBlock.Domain.Model;

namespace BuildingBlock.Domain.Repositories;

public interface IReadOnlyRepository<TEntity> where TEntity : GuidEntity
{
    Task<TEntity?> GetAnyAsync(ISpecification<TEntity> specification, string? includeTables);

    Task<List<TEntity>> GetAllAsync(ISpecification<TEntity> specification, string? includeTables);

    Task<bool> CheckIfExistAsync(ISpecification<TEntity> specification);

    Task<(List<TEntity>, int)> GetFilterAndPagingAsync(ISpecification<TEntity> specification, string? includeTables,
        string sort, int pageIndex, int pageSize);
}