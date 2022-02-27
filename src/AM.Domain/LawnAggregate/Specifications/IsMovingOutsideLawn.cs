using System.Linq.Expressions;
using AM.Domain.LawnAggregate.ValueObjects;
using AM.Domain.Shared.Specitifcations;
using AM.Domain.Shared.ValueObjects;

namespace AM.Domain.LawnAggregate.Specifications;

public class IsMovingOutsideLawn:Specification<Position>
{
    public override Expression<Func<Position,object[], bool>> ToExpression()
    {
        return (position, args) => 
            args[0] is Dimension &&
            position.X > ((Dimension) args[0]).Width ||
            position.Y > ((Dimension) args[0]).Height ||
            position.X <0 ||
            position.Y <0
            ;
    }
}