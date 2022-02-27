using AM.Application.AutomowerFeature.Dtos.Output;
using AM.Application.AutomowerFeature.Exceptions;
using AM.Application.AutomowerFeature.Formatters;
using AM.Domain.DomainServices.Abstractions;
using MediatR;

namespace AM.Application.AutomowerFeature.Commands.StartOperationCommand;

public class StartOperationCommandHandler:IRequestHandler<StartOperationCommand, OperationSummaryDto>
{
    private readonly ILawnDomainService _lawnDomainService;
    private readonly IFileFormatter _fileFormatter;

    public StartOperationCommandHandler(ILawnDomainService lawnDomainService, IFileFormatter fileFormatter)
    {
        _lawnDomainService = lawnDomainService;
        _fileFormatter = fileFormatter;
    }
    
    public Task<OperationSummaryDto> Handle(StartOperationCommand request, CancellationToken cancellationToken)
    {
        var startProcessCommand = _fileFormatter.Execute(request.File);
        startProcessCommand.LawnId = Guid.NewGuid();
        var lawn = _lawnDomainService.StartProcess(startProcessCommand);
        return Task.FromResult(OperationSummaryDto.FromMowersList(lawn.Mowers,request.IncludeMovementHistory));
    }

}
