using BuildingBlock.Domain.Repositories;
using Sales.Domain.Entities;

namespace Sales.Domain.Repositories.LeadRepository;

public interface ILeadReadOnlyRepository : IReadOnlyRepository<Lead>
{
}