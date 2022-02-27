namespace AM.Domain.Shared.ValueObjects;

/// <summary>
/// Dimension
/// </summary>
public class Dimension
{
    /// <summary>
    /// Width
    /// </summary>
    public int Width { get; private set; }

    /// <summary>
    /// Height
    /// </summary>
    public int Height { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <exception cref="Exception"></exception>
    public Dimension(int width, int height)
    {
        Width = width;
        Height = height;
    }
}