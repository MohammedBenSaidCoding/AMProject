using AM.Application.AutomowerFeature.Exceptions;
using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.Enums;
using AM.Domain.LawnAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;
using MowerAutomatic.Domain.LawnAggregate.Exceptions;
using System.Text.RegularExpressions;
using ApplicationConstants = AM.Application.Common.ApplicationConstants;

namespace AM.Application.AutomowerFeature.Formatters.Text;

public class TextFileFormatter:
    ITextFileFormatterLawnDimensionsSection,
    ITextFileFormatterReadFileSection,
    ITextFileFormatterCreateMowerCommandsSection
{
    public IFormFile File { get; private set; }

    public List<string> AllFilesLines { get; set; }

    private TextFileFormatter(IFormFile file)
    {
        File = file;
        AllFilesLines = new List<string>();
    }

    /// <summary>
    /// Create instance of TextFileFormatter
    /// </summary>
    /// <param name="file"></param>
    /// <returns>TextFileFormatter</returns>
    public static ITextFileFormatterReadFileSection CreateInstance(IFormFile file)
    {
        return new TextFileFormatter(file);
    }
    
    public ITextFileFormatterCreateMowerCommandsSection GetLawnDimensions( out int width, out int height)
    {
        if (AllFilesLines.Any() && RegexProvider.LawnDimensions.IsMatch(AllFilesLines[0])) 
        {
            var dimensionsArray = AllFilesLines[0].Split(ApplicationConstants.WhiteSpace);
            Int32.TryParse(dimensionsArray[0], out width);
            Int32.TryParse(dimensionsArray[1], out height);

            return this;
        }

        throw new LawInvalidDimensionsException(ConstantsAppMsgException.InvalidLawDimensions);
    }

    public ITextFileFormatterLawnDimensionsSection ReadFile()
    {
        using (var streamReader = new StreamReader(File.OpenReadStream()))
        {
            string? line;
            while (!string.IsNullOrWhiteSpace( line= streamReader?.ReadLine()) )
            {
                AllFilesLines.Add(line);
            }
        }

        if (AllFilesLines.Count ==0 || (AllFilesLines.Skip(1).Count()) % 2 != 0)
        {
            throw new InvalidFileContentException("The content of the file is invalid. please check the number of lines in the file.");
        }
        return this;
    }
    
    public List<CreateMowerCommand> CreateMowerCommands()
    {
        List<CreateMowerCommand> commands = new List<CreateMowerCommand>();
        
        int currentPosition = 1;
        int linesCount = AllFilesLines.Count;

        while (currentPosition<linesCount)
        {
            commands.Add(CreateMowerCommand(AllFilesLines, currentPosition, 2));
            currentPosition += 2;
        }

        return commands;
    }
    
     private static CreateMowerCommand CreateMowerCommand(List<string> lines, int skip, int take)
    {
        Position startingPosition;
        List<Instruction> instructions;
        (startingPosition,instructions) = ProcessFileLines(lines.Skip(skip).Take(take).ToList());
        return new CreateMowerCommand(Guid.NewGuid(),  startingPosition,  instructions);
    }

    private static (Position,List<Instruction>) ProcessFileLines(List<string> lines)
    {
        var startingPosition=new Position(0,0, Orientation.N);
        var instructions=new List<Instruction>();

        foreach (var fileLine in lines)
        {
            var upperLine =fileLine.ToUpper();
            
            if (ItIsInstruction(lines.IndexOf(fileLine)))
            {
                instructions = GetInstructions(upperLine);
            }
            else if(ItIsPosition(lines.IndexOf(fileLine)))
            {
                startingPosition = GetStartingPosition(upperLine);
            }
        }

        return (startingPosition, instructions);
    }

    private static List<Instruction>  GetInstructions(string fileLine)
    {
        fileLine = Regex.Replace(fileLine.ToUpper(), @"\s+", "");
        
        if (!RegexProvider.Instructions.IsMatch(fileLine))
        {
            throw new MowerInvalidInstructionException($"The instruction format ({fileLine}) is invalid. It must be composed of a sequence of the following letters: L, R, F");
        }
        
        var instructions = new List<Instruction>();
        var instructionsChars = fileLine.ToCharArray();

       foreach (var code in instructionsChars)
       {
           var instructionCode = Enum.Parse<InstructionCode>(code.ToString());
           instructions.Add(new Instruction(instructionCode));
       }

       return instructions;
    }
    private static Position GetStartingPosition(string fileLine)
    {
        if (!RegexProvider.StartingPosition.IsMatch(fileLine))
        {
            throw new MowerInvalidStartingPositionException($"The mower initial position format is invalid : {fileLine}");
        }
        
        var startingPositionArray = fileLine.Split(ApplicationConstants.WhiteSpace);
        
        //We have already checked the structure with the regular expression.
        return new  Position(int.Parse(startingPositionArray[0]),
            int.Parse(startingPositionArray[1]),
            Enum.Parse<Orientation>(startingPositionArray[2]),1);
    }

    private static bool ItIsInstruction(int index)
    {
        return (index+2)%2!=0;
    }
    
    private static bool ItIsPosition(int index)
    {
        return (index+2)%2==0;
    }
}