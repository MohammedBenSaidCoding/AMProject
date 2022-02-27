using System.ComponentModel;
using AM.Domain.LawnAggregate.Entities;
using AM.Domain.LawnAggregate.Enums;
using AM.Domain.LawnAggregate.Specifications;
using AM.Domain.Shared.ValueObjects;
using MowerAutomatic.Domain.LawnAggregate;

namespace AM.Domain.LawnAggregate.ValueObjects;

/// <summary>
/// Instruction
/// </summary>
public class Instruction
{
    /// <summary>
    /// The instruction code
    /// </summary>
    public  InstructionCode Code { get; private set; }

    /// <summary>
    /// The last move
    /// </summary>
    private MoveInstruction _lastMove;

    /// <summary>
    /// Instruction constructor
    /// </summary>
    /// <param name="code"></param>
    public Instruction(InstructionCode code)
    {
        Code = code;
    }

    /// <summary>
    /// Execute the instruction
    /// </summary>
    /// <param name="mower"></param>
    /// <param name="lawDimension"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    internal void Execute(Mower mower, Dimension lawDimension)
    {
        switch (Code)
        {
            case InstructionCode.R:
            case InstructionCode.L:
                Turn(mower);
                mower.AddMowerEvent(mower.Position);
                break;
            case InstructionCode.F:
                NextPosition(mower, lawDimension);
                mower.AddMowerEvent(mower.Position);
                break;
            case InstructionCode.Undefined:
            default:
                throw new InvalidEnumArgumentException( $"{nameof(Code)} : {Code}");
        }
    }

    private void NextPosition(Mower mower, Dimension lawnDimension)
    {
        switch (mower.Position.Orientation)
        {
            case Orientation.N:
                mower.Position.IncrementY();
                _lastMove = MoveInstruction.IncrementY;
                break;
            case Orientation.E:
               mower.Position.IncrementX();
                _lastMove = MoveInstruction.IncrementX;
                break;
            case Orientation.S:
                mower.Position.DecrementY();
                _lastMove = MoveInstruction.DecrementY;
                break;
            case Orientation.W:
                mower.Position.DecrementX();
                _lastMove = MoveInstruction.DecrementX;
                break;
            default:
                throw new InvalidEnumArgumentException($"{mower.Position.Orientation} : {mower.Position.Orientation}");
        }

        if (IsMovingOutsideLawn(mower, lawnDimension))
        {
            RollbackLastMove(mower);
        }
    }
    
    private void Turn(Mower mower)
    {
        switch (mower.Position.Orientation)
        {
            case Orientation.N:
                mower.Position.Turn(OrientationFromDirection(Orientation.N));
                break;
            case Orientation.E:
                mower.Position.Turn(OrientationFromDirection(Orientation.E));
                break;
            case Orientation.S:
                mower.Position.Turn(OrientationFromDirection(Orientation.S));
                break;
            case Orientation.W:
                mower.Position.Turn(OrientationFromDirection(Orientation.W));    
                break;
            default:
                throw new InvalidEnumArgumentException($"{nameof(mower.Position.Orientation)} : {mower.Position.Orientation}");
        }
    }

    private Orientation OrientationFromDirection(Orientation currentOrientation)
    {
        string combination= string.Format(Constants.OrientationDirectionCombination, currentOrientation, Code.ToString().ToUpper());

        switch (combination)
        {
            case Constants.Nr:
                return Orientation.E;
            case Constants.Nl:
                return Orientation.W;
            case Constants.Er:
                return Orientation.S;
            case Constants.El:
                return Orientation.N;
            case Constants.Sr:
                return Orientation.W;
            case Constants.Sl:
                return Orientation.E;
            case Constants.Wr:
                return Orientation.N;
            case Constants.Wl:
                return Orientation.S;
            default:
                throw new InvalidEnumArgumentException($"{nameof(combination)} : {combination}");
        }
    }

    private void RollbackLastMove(Mower mower)
    {
        switch (_lastMove)
        {
            case MoveInstruction.IncrementX:
                mower.Position.DecrementX();
                break;
            case MoveInstruction.DecrementX:
                mower.Position.IncrementX();
                break;
            case MoveInstruction.IncrementY:
                mower.Position.DecrementY();
                break;
            case MoveInstruction.DecrementY:
                mower.Position.IncrementY();
                break;
            default:
                throw new InvalidEnumArgumentException($"{nameof(_lastMove)} : {_lastMove}");
        }
    }
    
    /// <summary>
    /// Check if mower is moving outside of the lawn
    /// </summary>
    /// <param name="mower">mower</param>
    /// <param name="lawnDimension">lawn dimension</param>
    /// <returns></returns>
    private static bool IsMovingOutsideLawn(Mower mower, Dimension lawnDimension)
    {
        return new IsMovingOutsideLawn().IsSatisfiedBy(mower.Position, lawnDimension);
    }
}