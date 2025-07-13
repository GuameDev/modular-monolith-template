using ModularMonolith.Template.Domain.Common;
using ModularMonolith.Template.SharedKernel.Specification.Criterias;
using System.Linq.Expressions;

namespace ModularMonolith.Template.SharedKernel.Specification.Extensions;

public static class SpecificationExtensions
{
    public static ISpecification<T> And<T>(this ISpecification<T> specification, Criteria<T> criteria) where T : IAggregateRoot
    {
        var newCriteria = criteria.ToExpression();
        return specification.And(newCriteria);
    }

    public static ISpecification<T> Or<T>(this ISpecification<T> specification, Criteria<T> criteria) where T : IAggregateRoot
    {
        var newCriteria = criteria.ToExpression();
        return specification.Or(newCriteria);
    }

    public static ISpecification<T> Not<T>(this ISpecification<T> specification, Criteria<T> criteria)
    {
        var notCriteria = criteria.ToExpression().Not();
        specification.AddCriteria(notCriteria);
        return specification;
    }

    public static ISpecification<T> And<T>(this ISpecification<T> specification, Expression<Func<T, bool>> expression) where T : IAggregateRoot
    {
        if (specification.Criteria.Any())
        {
            var combined = specification.Criteria.First().And(expression);
            ((Specification<T>)specification).ClearCriteria();
            specification.AddCriteria(combined);
        }
        else
        {
            specification.AddCriteria(expression);
        }

        return specification;
    }

    public static ISpecification<T> Or<T>(this ISpecification<T> specification, Expression<Func<T, bool>> expression) where T : IAggregateRoot
    {
        if (specification.Criteria.Any())
        {
            var combined = specification.Criteria.First().Or(expression);
            ((Specification<T>)specification).ClearCriteria();
            specification.AddCriteria(combined);
        }
        else
        {
            specification.AddCriteria(expression);
        }

        return specification;
    }

    public static ISpecification<T> Not<T>(this ISpecification<T> specification, Expression<Func<T, bool>> expression)
    {
        var not = expression.Not();
        specification.AddCriteria(not);
        return specification;
    }
}
