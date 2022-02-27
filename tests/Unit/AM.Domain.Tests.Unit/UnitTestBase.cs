using System;
using FluentAssertions;

namespace AM.Domain.Tests.Unit;

public abstract class UnitTestBase
{
    public void AssertThrowException<T>(Action action, string message) where T : Exception, new()
    {
        action.Should()
                .Throw<T>()
                .WithMessage(message);
    }
}