using AM.Domain.LawnAggregate.Specifications;
using AM.Domain.LawnAggregate.ValueObjects;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;

namespace AM.Domain.LawnAggregate.Validators;

public static class LawnValidator
{
    public static void Validate(Lawn lawn)
    {
        if (lawn.Id == Guid.Empty)
        {
            throw new LawnArgumentException($"{nameof(lawn.Id)} is empty.");
        }
        else if(!lawn.Commands.Any())
        {
            throw new LawnEmptyCommandsException($"{nameof(lawn.Commands)} is empty. You must have at least one command.");
        }
        
        lawn.Commands.ForEach(MowerCommandValidator.Validate);
    }

}