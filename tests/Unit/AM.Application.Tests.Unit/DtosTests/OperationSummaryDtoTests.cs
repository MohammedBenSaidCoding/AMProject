using System;
using System.Collections.Generic;
using System.Linq;
using AM.Application.AutomowerFeature.Dtos.Output;
using AM.Domain.LawnAggregate.Entities;
using AM.Domain.LawnAggregate.Enums;
using AM.Domain.LawnAggregate.ValueObjects;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace AM.Application.Tests.Unit.DtosTests;

public class OperationSummaryDtoTests
{
    [Theory, AutoMoqData]
    public void FromMowersList_WithValidMowersListAndHistory_ReturnOperationSummaryDto(IFixture fixture)
    {
        var firstMower = fixture.Create<Mower>();
        var secondMower = fixture.Create<Mower>();
        
        var mowers = new List<Mower>() {firstMower,secondMower};
        var result=  OperationSummaryDto.FromMowersList(mowers,true);
        result.Should().NotBeNull();
        result.MowerSummaries.Should().NotBeNull();
        result.MowerSummaries.Count.Should().Be(mowers.Count);
        result.MowerSummaries.TrueForAll(x => x.MovementHistory != null).Should().BeTrue();
    }
    
    [Theory, AutoMoqData]
    public void FromMowersList_WithValidMowersListWithoutHistory_ReturnOperationSummaryDto(IFixture fixture)
    {
        var firstMower = fixture.Create<Mower>();
        var secondMower = fixture.Create<Mower>();
        
        var mowers = new List<Mower>() {firstMower,secondMower};
        var result=  OperationSummaryDto.FromMowersList(mowers,false);
        result.Should().NotBeNull();
        result.MowerSummaries.Should().NotBeNull();
        result.MowerSummaries.Count.Should().Be(mowers.Count);
        result.MowerSummaries.TrueForAll(x => x.MovementHistory == null).Should().BeTrue();
    }
}