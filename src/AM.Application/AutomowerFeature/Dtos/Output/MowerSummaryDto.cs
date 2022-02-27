using AM.Domain.LawnAggregate.ValueObjects;

namespace AM.Application.AutomowerFeature.Dtos.Output;

/// <summary>
/// Mower summary dto
/// </summary>
public class MowerSummaryDto
{
    /// <summary>
    /// Mower Id
    /// </summary>
    public Guid MowerId { get; set; }

    /// <summary>
    /// Mower position
    /// </summary>
    public PositionDto Position { get; set; } = new();

    /// <summary>
    /// Movement History
    /// </summary>
    public List<Position>? MovementHistory { get; set; }
}