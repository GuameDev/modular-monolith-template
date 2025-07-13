using ModularMonolith.Template.Domain.Common;
using ModularMonolith.Template.SharedKernel.Pagination;
using ModularMonolith.Template.SharedKernel.Specification.Criterias;
using System.Linq.Expressions;

namespace ModularMonolith.Template.SharedKernel.Specification
{
    public class Specification<TEntity> : ISpecification<TEntity> where TEntity : IAggregateRoot
    {
        private readonly List<Expression<Func<TEntity, bool>>> _criteria = new();
        private readonly List<Expression<Func<TEntity, object>>> _includes = new();
        private readonly List<string> _includeStrings = new();
        private readonly Dictionary<string, List<string>> _thenIncludeStrings = new();

        public IReadOnlyCollection<Expression<Func<TEntity, bool>>> Criteria => _criteria.AsReadOnly();
        public IReadOnlyCollection<Expression<Func<TEntity, object>>> Includes => _includes.AsReadOnly();
        public IReadOnlyCollection<string> IncludeStrings => _includeStrings.AsReadOnly();
        public IReadOnlyDictionary<string, List<string>> ThenIncludeStrings => _thenIncludeStrings;

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        public int? Page { get; private set; }
        public int? PageSize { get; private set; }

        public void AddCriteria(Expression<Func<TEntity, bool>> criteria)
        {
            _criteria.Add(criteria);
        }
        public void AddCriteria(Criteria<TEntity> criteria)
        {
            _criteria.Add(criteria.ToExpression());
        }

        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            if (!_includes.Contains(includeExpression))
            {
                _includes.Add(includeExpression);
            }
        }

        public void AddThenInclude<TPreviousProperty, TProperty>(
            Expression<Func<TEntity, TPreviousProperty>> includeExpression,
            Expression<Func<TPreviousProperty, TProperty>> thenIncludeExpression)
        {
            var includePath = GetPropertyPath(includeExpression);
            var thenIncludePath = GetPropertyPath(thenIncludeExpression);

            AddThenInclude(includePath, thenIncludePath);
        }

        private static string GetPropertyPath<TSource, TProperty>(Expression<Func<TSource, TProperty>> expression)
        {
            var path = new List<string>();
            Expression? current = expression.Body;

            while (current is MemberExpression memberExpression)
            {
                path.Insert(0, memberExpression.Member.Name);
                current = memberExpression.Expression;
            }

            return string.Join(".", path);
        }

        private void AddThenInclude(string include, string thenInclude)
        {
            if (!_thenIncludeStrings.ContainsKey(include))
            {
                _thenIncludeStrings[include] = new List<string>();
            }

            if (!_thenIncludeStrings[include].Contains(thenInclude))
            {
                _thenIncludeStrings[include].Add(thenInclude);
            }
        }


        public void ApplySorting(Expression<Func<TEntity, object>> orderByExpression, SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Ascending)
                OrderBy = orderByExpression;
            else
                OrderByDescending = orderByExpression;
        }


        public void ApplyPaging(int? page, int? pageSize)
        {
            page ??= PageListConstants.DefaultPage;
            pageSize ??= PageListConstants.DefaultPageSize;

            Page = page;
            PageSize = pageSize;
            Skip = (page.Value - 1) * pageSize.Value;
            Take = pageSize.Value;
            IsPagingEnabled = true;
        }

        internal void ClearCriteria() => _criteria.Clear();
    }
}
