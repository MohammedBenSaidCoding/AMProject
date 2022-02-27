using AM.Domain.LawnAggregate.Entities;

namespace AM.Application.AutomowerFeature.Dtos.Output;

public class OperationSummaryDto
{
    public List<MowerSummaryDto> MowerSummaries { get; set; } = new List<MowerSummaryDto>();
    
    public static OperationSummaryDto FromMowersList(List<Mower> mowers, bool includeHistory)
    {
        var operationSummary = new OperationSummaryDto();
        foreach (var mower in mowers)
        {
            operationSummary.MowerSummaries.Add(new MowerSummaryDto()
            {
                MowerId = mower.Id,
                Position = new PositionDto
                {
                    X=mower.Position.X,
                    Y = mower.Position.Y,
                    Orientation = mower.Position.Orientation
                },
                MovementHistory = includeHistory? mower.Events:null
            });
            
            
        }

        return operationSummary;
    }
}