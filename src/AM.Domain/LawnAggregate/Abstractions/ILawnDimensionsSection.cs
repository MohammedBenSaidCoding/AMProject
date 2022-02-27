namespace AM.Domain.LawnAggregate.Abstractions;

public interface ILawnDimensionsSection
{
    public IMowerCommandsSection SetDimensions(int width, int height);
}