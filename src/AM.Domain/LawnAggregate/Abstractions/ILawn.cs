namespace AM.Domain.LawnAggregate.Abstractions;

public interface ILawn:ILawnBuildSection,IMowerSection,IMowerCommandsSection,ILawnDimensionsSection, ILawnStart
{
}