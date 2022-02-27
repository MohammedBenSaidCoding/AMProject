using System;
using System.Collections.Generic;
using System.Linq;
using AM.Domain.LawnAggregate;
using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.Enums;
using AM.Domain.LawnAggregate.Exceptions;
using AM.Domain.LawnAggregate.ValueObjects;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;
using Xunit;

namespace AM.Domain.Tests.Unit.LawnTests;

public class LawnTests:UnitTestBase
{
    [Theory]
    [InlineAutoData(5,5, 0, 1, Orientation.N)]
    [InlineAutoData(7,9, 3, 4, Orientation.E)]
    public void CreateInstanceBuild_WithValidRequest_ReturnLawn(int width, int height,int x, int y, Orientation orientation, List<CreateMowerCommand> commands, Guid id, IFixture fixture)
    {
        //Given
        List<Instruction> instructions = fixture.Create<Generator<Instruction>>()
            .Where(x => x.Code != InstructionCode.Undefined).Take(4).ToList();

        var position= new Position(x, y, orientation);
        foreach (var command in commands)
        {
            command.StartingPosition = position;
            command.Instructions = instructions;
        }
        
        //When
        var lawn = Lawn
            .CreateInstance(id)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build();
   
       //Then
       lawn.Should().NotBeNull();
       lawn.Id.Should().Be(id);
       lawn.Dimensions.Should().NotBeNull();
       lawn.Dimensions.Height.Should().Be(height);
       lawn.Dimensions.Width.Should().Be(width);
       lawn.Commands.Should().NotBeNull();
       lawn.Commands.Count.Should().Be(commands.Count);
       lawn.Commands.Should().Equal(commands);
       lawn.Mowers.Should().NotBeNull();
       lawn.Mowers.ForEach(mower => mower.Position.Should().Be(position));
       lawn.Mowers.ForEach(mower => mower.Instructions.Should().Equal(instructions));
       lawn.Mowers.FirstOrDefault()?.Position.X.Should().BeLessOrEqualTo(width);
       lawn.Mowers.FirstOrDefault()?.Position.Y.Should().BeLessOrEqualTo(height);
       lawn.Mowers.FirstOrDefault()?.Position.X.Should().Be(x);
       lawn.Mowers.FirstOrDefault()?.Position.Y.Should().Be(y);
    }

    [Theory]
    [InlineAutoData(5,5, 1, 2, Orientation.N,1,3, Orientation.N, "LFLFLFLFF")]
    [InlineAutoData(5,5, 3, 3, Orientation.E,5, 1, Orientation.E,"FFRFFRFRRF")]
    public void CreateInstanceStart_WithValidRequest_ReturnLawn(int width, int height,int x, int y, Orientation orientation, int expectedX, int expectedY, Orientation expectedOrientation, string instructions, Guid id)
    {
        //Given
        var instructionsList = InstructionsFromString(instructions);
        var commands = new List<CreateMowerCommand>()
            { new (Guid.NewGuid(), new Position(x, y, orientation), instructionsList)};
       
        //When
        var lawn = Lawn
            .CreateInstance(id)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build()
            .Start();
   
        //Then
        lawn.Should().NotBeNull();
        lawn.Id.Should().Be(id);
        lawn.Dimensions.Should().NotBeNull();
        lawn.Dimensions.Height.Should().Be(height);
        lawn.Dimensions.Width.Should().Be(width);
        lawn.Commands.Should().NotBeNull();
        lawn.Commands.Count.Should().Be(commands.Count);
        lawn.Commands.Should().Equal(commands);
        lawn.Mowers.Should().NotBeNull();
        lawn.Mowers.ForEach(mower => mower.Instructions.Should().Equal(instructionsList));
        lawn.Mowers.ForEach(mower => mower.Id.Should().NotBe(Guid.Empty));
        lawn.Mowers.FirstOrDefault()?.Position.X.Should().BeLessOrEqualTo(width);
        lawn.Mowers.FirstOrDefault()?.Position.Y.Should().BeLessOrEqualTo(height);

        lawn.Mowers.First().Position.X.Should().Be(expectedX);
        lawn.Mowers.First().Position.Y.Should().Be(expectedY);
        lawn.Mowers.First().Position.Orientation.Should().Be(expectedOrientation);
        lawn.Mowers.First().Events?.Count.Should().Be(instructionsList.Count + 1);
    }

    [Theory]
    [InlineAutoData(5,5, 0, 2, Orientation.W,0,2, Orientation.W, "FFFFFFFFF")]
    [InlineAutoData(5,5, 0, 2, Orientation.S,0,0, Orientation.S, "FFFFFFFFF")]
    [InlineAutoData(5,5, 1, 2, Orientation.N,1,5, Orientation.N, "FFFFFFFFF")]
    [InlineAutoData(5,5, 3, 3, Orientation.E,5,3, Orientation.E,"FFFFFFFFF")]
    public void CreateInstanceStart_WithOutsideValidRequest_ReturnLawn(int width, int height,int x, int y, Orientation orientation, int expectedX, int expectedY, Orientation expectedOrientation, string instructions,  List<CreateMowerCommand> commands, Guid id)
    {
        //Given
        var instructionsList = InstructionsFromString(instructions);

        var expectedPosition = new Position(x, y, orientation);
        foreach (var command in commands)
        {
            command.StartingPosition = new Position(x, y, orientation) ;
            command.Instructions = instructionsList;
        }
        
        //When
        var lawn = Lawn
            .CreateInstance(id)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build()
            .Start();
   
        //Then
        lawn.Should().NotBeNull();
        lawn.Id.Should().Be(id);
        lawn.Dimensions.Should().NotBeNull();
        lawn.Dimensions.Height.Should().Be(height);
        lawn.Dimensions.Width.Should().Be(width);
        lawn.Commands.Should().NotBeNull();
        lawn.Commands.Count.Should().Be(commands.Count);
        lawn.Commands.Should().Equal(commands);
        lawn.Mowers.Should().NotBeNull();
        lawn.Mowers.ForEach(mower => mower.Instructions.Should().Equal(instructionsList));
        lawn.Mowers.FirstOrDefault()?.Position.X.Should().BeLessOrEqualTo(width);
        lawn.Mowers.FirstOrDefault()?.Position.Y.Should().BeLessOrEqualTo(height);

        lawn.Mowers.First().Position.X.Should().Be(expectedX);
        lawn.Mowers.First().Position.Y.Should().Be(expectedY);
        lawn.Mowers.First().Position.Orientation.Should().Be(expectedOrientation);
        lawn.Mowers.First().Events?.Count.Should().Be(instructionsList.Count + 1);
    }

    [Theory]
    [InlineAutoData(5,5, 1, 6,Orientation.N,"FFFFFFFFF")]
    [InlineAutoData(5,5, 6, 3,Orientation.E,"FFFFFFFFF")]
    public void CreateInstanceStart_WithStartingPositionOutsideValidRequest_MowerArgumentException(int width, int height,int x, int y, Orientation orientation, string instructions,  List<CreateMowerCommand> commands, Guid id)
    {
        //Given
        var instructionsList = InstructionsFromString(instructions);

        var expectedPosition = new Position(x, y, orientation);
        foreach (var command in commands)
        {
            command.StartingPosition = new Position(x, y, orientation) ;
            command.Instructions = instructionsList;
        }
        
        //When
        void Act()=> Lawn
            .CreateInstance(id)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build()
            .Start();
   
        //Then
        AssertThrowException<MowerArgumentException>(Act, string.Format(ConstantsMsgException.MowerStartingPositionOutOfLawn,expectedPosition.X, expectedPosition.Y));
        
    }

    
    [Theory]
    [InlineAutoData(0,1)]
    [InlineAutoData(1,0)]
    [InlineAutoData(-2,-1)]
    [InlineAutoData(0,0)]
    [InlineAutoData(-1,2)]
    public void CreateInstanceBuild_WithInvalidLawnDimension_ThrowLawInvalidDimensionsException(int width, int height,int x, int y, Orientation orientation, List<CreateMowerCommand> commands, Guid id, IFixture fixture)
    {
        //Given
        List<Instruction> instructions = fixture.Create<Generator<Instruction>>()
            .Where(x => x.Code != InstructionCode.Undefined).Take(4).ToList();

        var position= new Position(x, y, orientation);
        foreach (var command in commands)
        {
            command.StartingPosition = position;
            command.Instructions = instructions;
        }
        
        //When
        void Act()=> Lawn
            .CreateInstance(id)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build();

        //Then
        AssertThrowException<LawInvalidDimensionsException>(Act, string.Format(ConstantsMsgException.InvalidLawDimensions, width,height));

    }
    
    [Theory]
    [InlineAutoData(5,5)]
    public void CreateInstanceBuild_WithEmptyCommands_ThrowLawnEmptyCommandsException(int width, int height, Guid id)
    {
        //Given
        var commands = new List<CreateMowerCommand>();
        
        //When
        void Act()=> Lawn
            .CreateInstance(id)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build();

        //Then
        AssertThrowException<LawnEmptyCommandsException>(Act, $"Commands is empty. You must have at least one command.");

    }

    [Theory]
    [InlineAutoData(5,5, 0, 1, Orientation.N)]
    [InlineAutoData(7,9, 3, 4, Orientation.E)]
    public void CreateInstanceBuild_WithEmptyInstructions_ThrowMowerArgumentException(int width, int height,int x, int y, Orientation orientation, List<CreateMowerCommand> commands, Guid id)
    {
        //Given

        var position= new Position(x, y, orientation);
        foreach (var command in commands)
        {
            command.StartingPosition = position;
            command.Instructions = new List<Instruction>();
        }
        
        //When
        void Act() =>
            Lawn.CreateInstance(id)
                .SetDimensions(width, height)
                .SetControlInstructions(commands)
                .CreateMowers()
                .Build();

        //Then
        AssertThrowException<MowerArgumentException>(Act, $"For each mower there must be at least one instruction.");
        
    }

    [Theory]
    [InlineAutoData(5,5)]
    public void CreateInstanceBuild_WithEmptyLawnId_ThrowLawnArgumentException(int width, int height)
    {
        //Given

        var commands = new List<CreateMowerCommand>();
        
        //When
        void Act()=> Lawn
            .CreateInstance(Guid.Empty)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build();

        AssertThrowException<LawnArgumentException>(Act, $"Id is empty.");
    }

    [Theory]
    [InlineAutoData(5,5, 0, 1, Orientation.N)]
    [InlineAutoData(7,9, 3, 4, Orientation.E)]
    public void CreateInstanceBuild_WithEmptyMowerId_ThrowMowerArgumentException(int width, int height,int x, int y, Orientation orientation, List<CreateMowerCommand> commands)
    {
        //Given
        var position= new Position(x, y, orientation);
        foreach (var command in commands)
        {
            command.MowerId = Guid.Empty;
            command.StartingPosition = position;
            command.Instructions = new List<Instruction>();
        }
        
        //When
        void Act()=> Lawn
            .CreateInstance(Guid.Empty)
            .SetDimensions(width, height)
            .SetControlInstructions(commands)
            .CreateMowers()
            .Build();

        //Then
        AssertThrowException<MowerArgumentException>(Act, string.Format(ConstantsMsgException.IsNull,
            "MowerId"));

    }
    
    private List<Instruction> InstructionsFromString(string instructionsAsString)
    {
        var instructions = new List<Instruction>();
        var instructionsChars = instructionsAsString.ToCharArray();

        foreach (var code in instructionsChars)
        {
            Enum.TryParse(code.ToString(), true, out InstructionCode instructionCode);
            instructions.Add(new Instruction(instructionCode));
        }

        return instructions;
    }
   
}
