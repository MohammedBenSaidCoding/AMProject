using AM.Domain.Shared.ValueObjects;

namespace AM.Domain.LawnAggregate.Commands;

public class StartProcessCommand
{
    public StartProcessCommand(Guid lawnId, Dimension lawnDimensions, List<CreateMowerCommand> commands)
    {
        LawnId = lawnId;
        LawnDimensions = lawnDimensions;
        Commands = commands;
    }

    public Guid LawnId { get; set; }

    public Dimension LawnDimensions { get; set; }

    public List<CreateMowerCommand> Commands { get; set; }
}