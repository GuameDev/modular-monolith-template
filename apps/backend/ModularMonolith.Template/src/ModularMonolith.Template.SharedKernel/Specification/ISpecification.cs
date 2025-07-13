using ModularMonolith.Template.SharedKernel.Pagination;
using System.Linq.Expressions;

namespace ModularMonolith.Template.SharedKernel.Specification
{
    public interface ISpecification<T>
    {
        IReadOnlyCollection<Expression<Func<T, bool>>> Criteria { get; }
        IReadOnlyCollection<Expression<Func<T, object>>> Includes { get; }
        IReadOnlyCollection<string> IncludeStrings { get; }
        IReadOnlyDictionary<string, List<string>> ThenIncludeStrings { get; }

        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }

        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        int? Page { get; }
        int? PageSize { get; }

        void AddCriteria(Expression<Func<T, bool>> criteria);
        void ApplySorting(Expression<Func<T, object>> orderByExpression, SortDirection sortDirection);
        void ApplyPaging(int? page, int? pageSize);
        void AddInclude(Expression<Func<T, object>> includeExpression);
        void AddThenInclude<TPreviousProperty, TProperty>(
            Expression<Func<T, TPreviousProperty>> includeExpression,
            Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression);

    }
}
