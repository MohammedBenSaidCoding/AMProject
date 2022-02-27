using AM.Domain.DomainServices.Abstractions;
using AM.Domain.LawnAggregate;
using AM.Domain.LawnAggregate.Commands;

namespace AM.Domain.DomainServices;

public class LawnDomainService:ILawnDomainService
{
    /// <summary>
    /// Start processs
    /// </summary>
    /// <param name="startProcessCommand"></param>
    /// <returns>Lawn</returns>
    public Lawn StartProcess(StartProcessCommand startProcessCommand)
    {
        return Lawn.CreateInstance(Guid.NewGuid())
            .SetDimensions(startProcessCommand.LawnDimensions.Width, startProcessCommand.LawnDimensions.Height)
            .SetControlInstructions(startProcessCommand.Commands)
            .CreateMowers()
            .Build()
            .Start();
    }
}