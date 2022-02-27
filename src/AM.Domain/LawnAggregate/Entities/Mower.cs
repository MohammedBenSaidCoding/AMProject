using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.Validators;
using AM.Domain.LawnAggregate.ValueObjects;
using AM.Domain.Shared.ValueObjects;

namespace AM.Domain.LawnAggregate.Entities;

/// <summary>
/// Mower class
/// </summary>
public class Mower
{
    /// <summary>
    /// Mower ID
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Mower position
    /// </summary>
    public Position Position { get; set; }

    /// <summary>
    /// Mower events
    /// </summary>
    private List<Position>? _events { get; set; }

    /// <summary>
    /// Mower events
    /// </summary>
    public List<Position>? Events => _events;

    /// <summary>
    /// Mower instructions
    /// </summary>
    private readonly List<Instruction> _instructions = new List<Instruction>();
    
    /// <summary>
    /// Mower instructions
    /// </summary>
    public List<Instruction> Instructions => _instructions;

    /// <summary>
    /// Mower constructor
    /// </summary>
    /// <param name="createMowerCommand"></param>
    public Mower(CreateMowerCommand createMowerCommand)
    {
        MowerCommandValidator.Validate(createMowerCommand);
        
        Id = createMowerCommand.MowerId;
        Position = createMowerCommand.StartingPosition;
        _instructions.AddRange(createMowerCommand.Instructions);
        _events = new List<Position> {new Position(Position.X, Position.Y, Position.Orientation)};
    }

    /// <summary>
    /// Execute all mower's instructions
    /// </summary>
    /// <param name="lawDimension"></param>
    public void Run(Dimension lawDimension)
    {
        foreach (var instruction in _instructions)
        {
            instruction.Execute(this,lawDimension);
        }
    }
    
    /// <summary>
    /// Add change position as event
    /// </summary>
    /// <param name="position"></param>
    internal void AddMowerEvent(Position position)
    {
        _events?.Add(new Position(position.X, position.Y, position.Orientation));
    }
}