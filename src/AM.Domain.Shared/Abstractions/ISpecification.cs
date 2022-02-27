using System.Linq.Expressions;

namespace AM.Domain.Shared.Abstractions;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T obj,params object[] args);

    Expression<Func<T,object[], bool>> ToExpression();
}