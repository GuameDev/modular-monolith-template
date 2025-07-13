
namespace ModularMonolith.Template.SharedKernel.Specification
{
    public interface ISpecificationEvaluator<TEntity> where TEntity : class
    {
        public static abstract IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification);
    }
}