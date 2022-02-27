using System.Linq.Expressions;
using AM.Domain.Shared.Specitifcations;

namespace AM.Domain.LawnAggregate.Specifications;

public class IsValidLawDimensions:Specification<Lawn>
{
    public override Expression<Func<Lawn, object[], bool>> ToExpression()
    {
        return (lawn, args) =>
             lawn.Dimensions.Height > 0 && lawn.Dimensions.Width > 0;
    }
}