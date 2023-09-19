namespace BuildingBlock.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();

    void BeginTransaction();

    void Commit();

    void Rollback();
}