using System.Linq.Expressions;

namespace ModularMonolith.Template.SharedKernel.Specification.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return CombineExpressions(first, second, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return CombineExpressions(first, second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var parameter = expression.Parameters[0];
            var body = Expression.Not(expression.Body);

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression<Func<T, bool>> CombineExpressions<T>(
            Expression<Func<T, bool>> first,
            Expression<Func<T, bool>> second,
            Func<Expression, Expression, Expression> merge)
        {
            var parameter = Expression.Parameter(typeof(T));

            var firstBody = new ReplaceParameterVisitor(first.Parameters[0], parameter).Visit(first.Body);
            var secondBody = new ReplaceParameterVisitor(second.Parameters[0], parameter).Visit(second.Body);

            var body = merge(firstBody!, secondBody!);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

    }
}
