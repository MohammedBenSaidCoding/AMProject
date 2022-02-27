using AM.Application.AutomowerFeature.Dtos.Output;
using AM.Domain.LawnAggregate.ValueObjects;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AM.Application.Tests.Unit.DtosTests;

public class PositionDtoTests
{
    [Theory, AutoMoqData]
    public void FromPositionDomain_WithValidPosition_ReturnPositionDto(Position position)
    {
       var result= PositionDto.FromPositionDomain(position);
       result.Should().NotBeNull();
       result.Should().BeEquivalentTo(position);
    }
    
}