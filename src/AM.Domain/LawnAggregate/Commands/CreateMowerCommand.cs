using AM.Domain.LawnAggregate.ValueObjects;

namespace AM.Domain.LawnAggregate.Commands;

public class CreateMowerCommand
{
    public Guid MowerId { get; set; }

    public Position StartingPosition { get; set; }

    public List<Instruction> Instructions { get; set; }
    
    public CreateMowerCommand(Guid mowerId, Position startingPosition, List<Instruction> instructions)
    {
        MowerId = mowerId;
        StartingPosition = startingPosition;
        Instructions = instructions;
    }
}