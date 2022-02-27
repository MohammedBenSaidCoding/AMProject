using AM.Domain.LawnAggregate.Exceptions;
using AM.Domain.LawnAggregate.Specifications;
using AM.Domain.LawnAggregate.ValueObjects;
using AM.Domain.Shared.ValueObjects;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;

namespace AM.Domain.LawnAggregate.Validators;

public static class MowerPositionValidator
{
    public static void Validate(Position position, Dimension lawnDimension)
    {
        if (new IsMovingOutsideLawn().IsSatisfiedBy(position, lawnDimension))
        {
            throw new MowerArgumentException(string.Format(ConstantsMsgException.MowerStartingPositionOutOfLawn,position.X, position.Y));
        }
    }
    
}