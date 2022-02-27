using AM.Domain.LawnAggregate.Commands;
using AM.Domain.LawnAggregate.ValueObjects;

namespace AM.Domain.LawnAggregate.Abstractions;

public interface IMowerCommandsSection
{
  public IMowerSection SetControlInstructions(List<CreateMowerCommand> commands);
}