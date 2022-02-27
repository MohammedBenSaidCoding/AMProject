using AM.Domain.LawnAggregate.Enums;
using AM.Domain.LawnAggregate.ValueObjects;

namespace AM.Application.AutomowerFeature.Dtos.Output;

public class PositionDto
{
    public int X { get; set; }

    public int Y { get; set; }

    public Orientation Orientation { get; set; }

    public static PositionDto FromPositionDomain(Position position)
    {
        return new PositionDto()
        {
            X = position.X,
            Y = position.Y,
            Orientation = position.Orientation
        };
    }
}