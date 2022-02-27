using AM.Application.AutomowerFeature.Exceptions;
using AM.Application.AutomowerFeature.Formatters.Text;
using AM.Domain.LawnAggregate.Commands;
using AM.Domain.Shared.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace AM.Application.AutomowerFeature.Formatters;

public class TextFileFormatterRunner:IFileFormatter
{
    public StartProcessCommand Execute(IFormFile? file)
    {
        if (file is null)
            throw new InvalidRequestException(ConstantsAppMsgException.StartOperationCommandFileNull);
        
        var commands  = TextFileFormatter
            .CreateInstance(file)
            .ReadFile()
            .GetLawnDimensions(out int width, out int height)
            .CreateMowerCommands();

        return new StartProcessCommand(Guid.Empty,
            new Dimension(width,height),
            commands);
    }
}