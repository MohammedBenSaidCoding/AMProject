namespace AM.Application.AutomowerFeature.Formatters.Text;

public interface ITextFileFormatterLawnDimensionsSection
{
    ITextFileFormatterCreateMowerCommandsSection GetLawnDimensions(out int width, out int height);
}