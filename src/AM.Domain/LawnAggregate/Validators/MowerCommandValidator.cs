using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.Exceptions;
using AM.Domain.LawnAggregate.ValueObjects;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;

namespace AM.Domain.LawnAggregate.Validators;

public static class MowerCommandValidator
{
    public static void Validate(CreateMowerCommand? createMowerCommand)
    {
        if (createMowerCommand is null)
        {
            throw new MowerArgumentException(string.Format(ConstantsMsgException.IsNull,
                nameof(createMowerCommand)));
        }
        else if (createMowerCommand.MowerId == Guid.Empty)
        {
            throw new MowerArgumentException(string.Format(ConstantsMsgException.IsNull,
                nameof(createMowerCommand.MowerId)));
        }
        else if (!createMowerCommand.Instructions.Any())
        {
            throw new MowerArgumentException("For each mower there must be at least one instruction.");
        }
    }
}