using BuildingBlock.Domain.Exceptions;
using Sales.Domain.LeadAggregate.Enums;

namespace Sales.Domain.LeadAggregate.Exceptions;

public class LeadValidStatusException : EntityValidationException
{
    public LeadValidStatusException(LeadStatus status) : base(nameof(Lead) + $"[{status}]" +
                                                              $" status is not {LeadStatus.Open}/{LeadStatus.Prospect}")
    {
    }
}