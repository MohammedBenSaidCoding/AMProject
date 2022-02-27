using AM.Domain.LawnAggregate;
using AM.Domain.LawnAggregate.Commands;

namespace AM.Domain.DomainServices.Abstractions;

public interface ILawnDomainService
{
    Lawn StartProcess(StartProcessCommand startProcessCommand);
}