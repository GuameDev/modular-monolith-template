using System.Linq.Expressions;

namespace ModularMonolith.Template.SharedKernel.Specification.Criterias
{
    public abstract class Criteria<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
