using AM.Domain.LawnAggregate.Commands;

namespace AM.Application.AutomowerFeature.Formatters.Text;

public interface ITextFileFormatterCreateMowerCommandsSection
{
    List<CreateMowerCommand> CreateMowerCommands();
}