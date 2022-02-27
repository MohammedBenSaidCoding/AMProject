using System.Diagnostics.CodeAnalysis;
using AM.Application.AutomowerFeature.Commands.StartOperationCommand;
using AM.Application.AutomowerFeature.Exceptions;
using FluentValidation;

namespace AM.Application.Common.Validators;
[ExcludeFromCodeCoverage]
public class StartOperationCommandValidator:AbstractValidator<StartOperationCommand>
{
    public StartOperationCommandValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .NotEmpty()
            .WithMessage(ConstantsAppMsgException.StartOperationCommandNull);
        
        RuleFor(x => x.File)
            .NotNull()
            .NotEmpty()
            .WithMessage(ConstantsAppMsgException.StartOperationCommandFileNull);
        
        RuleFor(x => x.File)
            .Must(x=>x is {ContentType: "text/plain"})
            .WithMessage(ConstantsAppMsgException.InvalidFileContentType);
           

        RuleFor(x => x.IncludeMovementHistory)
            .Must(x => x is bool)
            .WithMessage("Invalid val");
    }
}