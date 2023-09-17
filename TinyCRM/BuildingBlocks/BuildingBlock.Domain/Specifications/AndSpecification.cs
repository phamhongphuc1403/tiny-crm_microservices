﻿using System.Linq.Expressions;

namespace BuildingBlock.Domain.Specifications;

public class AndSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var rightBody =
            ExpressionParameterReplacer.ReplaceParameters(rightExpression.Body, leftExpression.Parameters[0]);

        var body = Expression.AndAlso(leftExpression.Body, rightBody);
        var lambda = Expression.Lambda<Func<T, bool>>(body, leftExpression.Parameters);

        return lambda;
    }
}