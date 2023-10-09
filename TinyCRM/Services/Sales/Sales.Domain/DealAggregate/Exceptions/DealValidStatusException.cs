using BuildingBlock.Domain.Exceptions;
using Sales.Domain.DealAggregate.Enums;

namespace Sales.Domain.DealAggregate.Exceptions;

public class DealValidStatusException : EntityValidationException
{
    public DealValidStatusException(DealStatus status) : base(nameof(DealStatus) + $"[{status}]" +
                                                              $" status is not {DealStatus.Open}")
    {
    }
}