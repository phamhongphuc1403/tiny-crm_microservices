namespace BuildingBlock.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync();

    void BeginTransaction();

    void Commit();

    void Rollback();
}