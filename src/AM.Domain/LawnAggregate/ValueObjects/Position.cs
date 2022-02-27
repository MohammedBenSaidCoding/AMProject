using AM.Domain.LawnAggregate.Abstractions;
using AM.Domain.LawnAggregate.Enums;

namespace AM.Domain.LawnAggregate.ValueObjects;

/// <summary>
/// Position : X-axis, Y-axis
/// </summary>
public class Position:IPosition
{
    /// <summary>
    /// The X-axis
    /// </summary>
    public int X { get; private set; }
    
    /// <summary>
    /// The Y-axis
    /// </summary>
    public int Y { get; private set; }
    
    /// <summary>
    /// Orientation
    /// </summary>
    public Orientation Orientation { get; private set; }

    /// <summary>
    /// Move step
    /// </summary>
    private  readonly int _step;


    /// <summary>
    /// Position constructor
    /// </summary>
    /// <param name="x">x-axis</param>
    /// <param name="y">y-axis</param>
    /// <param name="orientation">Orientation</param>
    /// <param name="step">move step</param>
    public Position(int x, int y, Orientation orientation, int step=1)
    {
        X = x;
        Y = y;
        Orientation = orientation;
        _step = step;
    }

    public  IPosition IncrementX()
    {
        X += _step;
        return this;
    }

    public IPosition DecrementX()
    {
        X -= _step;
        return this;
    }

    public IPosition IncrementY()
    {
        Y += _step;
        return this;
    }

    public IPosition DecrementY()
    {
        Y -= _step;
        return this;
    }

    /// <summary>
    /// Turn : Change orientation
    /// </summary>
    /// <param name="newOrientation"></param>
    /// <returns></returns>
    public IPosition Turn(Orientation newOrientation)
    {
        Orientation = newOrientation;
        return this;
    }

}