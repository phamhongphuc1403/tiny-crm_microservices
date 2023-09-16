using System.Linq.Expressions;

namespace BuildingBlock.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();

    Specification<T> And(Specification<T> specification);
    Specification<T> Or(Specification<T> specification);
}