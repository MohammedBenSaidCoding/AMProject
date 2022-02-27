using System;
using System.IO;
using System.Linq;
using System.Text;
using AM.Application.AutomowerFeature.Exceptions;
using AM.Application.AutomowerFeature.Formatters;
using AM.Application.AutomowerFeature.Formatters.Text;
using AM.Domain.LawnAggregate.Enums;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;
using Xunit;

namespace AM.Application.Tests.Unit.FormattersTests;

public class TextFileFormatterTests:UnitTestBase
{
    [Theory,AutoMoqData]
    public void CreateInstance_WithValidFile_CreateTextFileFormatter([Frozen] Mock<IFormFile> file)
    { 
        //When
       var textFileFormatter = (TextFileFormatter) TextFileFormatter.CreateInstance(file.Object);
       
       //Then
       textFileFormatter.Should().NotBeNull();
       textFileFormatter.Should().BeOfType<TextFileFormatter>();
       textFileFormatter.File.Should().NotBeNull();
       textFileFormatter.File.Should().Be(file.Object);
       textFileFormatter.AllFilesLines.Should().NotBeNull();
    }

    [Fact]
    public void ReadFile_WithValidFile_FillAllFilesLines()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/ValidFile.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        var textFileFormatter = (TextFileFormatter) TextFileFormatter.CreateInstance(file).ReadFile();
        
        //Then
        textFileFormatter.Should().BeOfType<TextFileFormatter>();
        textFileFormatter.AllFilesLines.Should().NotBeNull();
        textFileFormatter.AllFilesLines.Any().Should().BeTrue();
    }
    
    [Fact]
    public void ReadFile_WithInvalidNumberOfLines_ThrowInvalidFileContentException()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/FileWithInvalidNumberOfLines.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        void Act()=> TextFileFormatter.CreateInstance(file).ReadFile();
        
        //Then
        AssertThrowException<InvalidFileContentException>(Act, "The content of the file is invalid. please check the number of lines in the file.");
    }
    
    [Fact]
    public void ReadFile_WithFileWithoutInstructions_ThrowInvalidFileContentException()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/FileWithoutMowerInstructions.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        void Act()=> TextFileFormatter.CreateInstance(file).ReadFile();
        
        //Then
        AssertThrowException<InvalidFileContentException>(Act, "The content of the file is invalid. please check the number of lines in the file.");
    }
    

    [Fact]
    public void GetLawnDimensions_WithValidFile_FillWidthAndHeight()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/ValidFile.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        var textFileFormatter = (TextFileFormatter) TextFileFormatter
            .CreateInstance(file)
            .ReadFile()
            .GetLawnDimensions(out var width, out var height);
        
        //Then
        textFileFormatter.Should().BeOfType<TextFileFormatter>();
        width.Should().Be(6);
        height.Should().Be(8);
    }
    
    [Fact]
    public void GetLawnDimensions_WithInvalidDimensions_ThrowLawInvalidDimensionsException()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/FileWithInvalidDimensions.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        void Act()=>TextFileFormatter
            .CreateInstance(file)
            .ReadFile()
            .GetLawnDimensions(out var width, out var height);
        
        //Then
        AssertThrowException<LawInvalidDimensionsException>(Act, ConstantsAppMsgException.InvalidLawDimensions);

    }

    [Fact]
    public void CreateMowerCommands_WithValidFile_ReturnListOfCreateMowerCommand()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/ValidFile.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        var createMowerCommands =  TextFileFormatter
            .CreateInstance(file)
            .ReadFile()
            .GetLawnDimensions(out var width, out var height)
            .CreateMowerCommands();
        
        //Then
        createMowerCommands.Should().NotBeNull();
        createMowerCommands.Count.Should().Be(2);
        createMowerCommands.First().MowerId.Should().NotBe(Guid.Empty);
        createMowerCommands.First().Instructions.Count.Should().Be(9);
        createMowerCommands.First().StartingPosition.Orientation.Should().Be(Orientation.N);
        createMowerCommands.First().StartingPosition.X.Should().Be(3);
        createMowerCommands.First().StartingPosition.Y.Should().Be(1);
        
        createMowerCommands.Last().MowerId.Should().NotBe(Guid.Empty);
        createMowerCommands.Last().Instructions.Count.Should().Be(6);
        createMowerCommands.Last().StartingPosition.Orientation.Should().Be(Orientation.E);
        createMowerCommands.Last().StartingPosition.X.Should().Be(5);
        createMowerCommands.Last().StartingPosition.Y.Should().Be(2);

    }
    
    [Fact]
    public void CreateMowerCommands_WithInvalidStartingPosition_ThrowMowerInvalidStartingPositionException()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/FileWithInvalidStartingPosition.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        void Act()=> TextFileFormatter
            .CreateInstance(file)
            .ReadFile()
            .GetLawnDimensions(out var width, out var height)
            .CreateMowerCommands();
        
        //Then
        AssertThrowException<MowerInvalidStartingPositionException>(Act, $"The mower initial position format is invalid : 3 1 T");

      

    }

    [Fact]
    public void CreateMowerCommands_WithInvalidInstructions_ThrowMowerInvalidInstructionException()
    {
        //Given
        var bytes = File.ReadAllBytes(@"Data/FileWithInvalidInstructions.txt");
        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "ValidFile.txt");

        //When
        void Act()=> TextFileFormatter
            .CreateInstance(file)
            .ReadFile()
            .GetLawnDimensions(out var width, out var height)
            .CreateMowerCommands();
        
        //Then
        AssertThrowException<MowerInvalidInstructionException>(Act, $"The instruction format (FFRFFFLZF) is invalid. It must be composed of a sequence of the following letters: L, R, F");

      

    }

}