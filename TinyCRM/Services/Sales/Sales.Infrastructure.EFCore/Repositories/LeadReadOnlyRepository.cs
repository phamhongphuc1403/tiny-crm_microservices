using BuildingBlock.Infrastructure.EFCore.Repositories;
using Sales.Domain.Entities;
using Sales.Domain.Repositories.LeadRepository;

namespace Sales.Infrastructure.EFCore.Repositories;

public class LeadReadOnlyRepository : ReadOnlyRepository<SaleDbContext, Lead>, ILeadReadOnlyRepository
{
    public LeadReadOnlyRepository(SaleDbContext dbContext) : base(dbContext)
    {
    }
}