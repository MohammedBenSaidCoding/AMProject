using AM.Domain.LawnAggregate.Exceptions;
using AM.Domain.LawnAggregate.Validators;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;
using Xunit;

namespace AM.Domain.Tests.Unit.ValidatorsTests;

public class MowerCommandValidatorTests:UnitTestBase
{
    [Fact]
    public void Validate_WithNullCreateMowerCommand_ThrowMowerArgumentException()
    {
        void Act() => MowerCommandValidator.Validate(null);
        
        AssertThrowException<MowerArgumentException>(Act, string.Format(ConstantsMsgException.IsNull,
            "createMowerCommand"));
    }
}