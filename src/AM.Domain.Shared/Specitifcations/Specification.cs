using System.Linq.Expressions;
using AM.Domain.Shared.Abstractions;

namespace AM.Domain.Shared.Specitifcations;

public abstract class Specification<T>:ISpecification<T>
{
    public abstract Expression<Func<T,object[], bool>> ToExpression();
    
    public virtual bool IsSatisfiedBy(T obj, params object[] args)
    {
        return ToExpression().Compile()(obj,args);
    }

    public static implicit operator Expression<Func<T,object[],bool>>(Specification<T> specification)
    {
        return specification.ToExpression();
    }
}