using System;
using System.Collections.Generic;
using AM.Domain.DomainServices;
using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.Enums;
using AM.Domain.LawnAggregate.ValueObjects;
using FluentAssertions;
using Xunit;

namespace AM.Domain.Tests.Unit.ServicesTests;

public class LawnDomainServiceTests
{
    [Theory, AutoMoqData]
    public void StartProcess_WithValidCommand_ReturnLawn(StartProcessCommand command,
        LawnDomainService lawnDomainService)
    {
       //Given
        foreach (var createMowerCommand in command.Commands)
        {
            createMowerCommand.MowerId= Guid.NewGuid();
            createMowerCommand.Instructions = new List<Instruction>()
            {
                new Instruction(InstructionCode.L)
            };
            createMowerCommand.StartingPosition = new Position(1, 1, Orientation.E);
        }
      
        //when
      var result = lawnDomainService.StartProcess(command);
        
        //Then
        result.Should().NotBeNull();
        result.Id.Should().NotBe(Guid.Empty);
        result.Mowers.Should().NotBeNull();
        result.Mowers.Count.Should().Be(command.Commands.Count);
    }
}